using Application.DTOs.Auth;
using Application.Exceptions;
using Application.Helpers;
using Application.Persistence;
using Application.Services.IServices;
using Domain.Entities.identity;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.auth;
using AutoMapper;
using Application.DTOs.User;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Application.Infrastructure;
using Application.Models;
using Microsoft.Extensions.Options;
using MimeKit;
using Microsoft.AspNetCore.WebUtilities;
using System.Data.Entity;
using System.Globalization;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace persistence.Services
{
    public class AuthServices : IAuthService
    {
        private AppDbContext _context;
        private IJwtService _jwtUtils;
        private IUnitOfWork _unitOfWork;
        private AppSettings _appSettings;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        public AuthServices(AppDbContext context, IOptions<AppSettings> appSettings, IEmailSender emailSender, IJwtService jwtUtils, IUnitOfWork unitOfWork, Microsoft.AspNetCore.Identity.UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
            _userManager = userManager;
            _mapper = mapper;
            _emailSender = emailSender;

        }
        public async Task<AuthenticateResponse> AuthenticateAsync(LoginDto model, string ipAddress)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == model.Email);

            // validate
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                throw new BadRequestException("Username or password is incorrect");

            if (user.EmailConfirmed == false)
                throw new BadRequestException("Please Confirme Your Email");
            var roles = await _userManager.GetRolesAsync(user);

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = _jwtUtils.GenerateJwtToken(user, roles);
            var refreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            if(user.RefreshTokens is null)
            {
                user.RefreshTokens = new List<RefreshToken>();
            }
            user.RefreshTokens.Add(refreshToken);
            // remove old refresh tokens from user
            removeOldRefreshTokens(user);

            // save changes to db
            _context.Update(user);
            _context.SaveChanges();
            return new AuthenticateResponse
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                JwtToken = jwtToken,
                RefreshToken = refreshToken.Token
            };

        }
        public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {
            var user = getUserByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (refreshToken.IsRevoked)
            {
                // revoke all descendant tokens in case this token has been compromised
                revokeDescendantRefreshTokens(refreshToken, user, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
                _context.Update(user);
                _context.SaveChanges();
            }

            if (!refreshToken.IsActive)
                throw new BadRequestException("Invalid token");

            // replace old refresh token with a new one (rotate token)
            var newRefreshToken = rotateRefreshToken(refreshToken, ipAddress);
            user.RefreshTokens.Add(newRefreshToken);

            // remove old refresh tokens from user
            removeOldRefreshTokens(user);

            // save changes to db
            _context.Update(user);
            _context.SaveChanges();

            var roles = await _userManager.GetRolesAsync(user);

            // generate new jwt
            var jwtToken = _jwtUtils.GenerateJwtToken(user, roles);

            return new AuthenticateResponse
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                JwtToken = jwtToken,
                RefreshToken = refreshToken.Token
            };
        }

        public void RevokeToken(string token, string ipAddress)
        {
            var user = getUserByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
                throw new BadRequestException("Invalid token");

            // revoke token and save
            revokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
            _context.Update(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(string id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

        // helper methods

        private User getUserByRefreshToken(string token)
        {
           
           var user = _context.Users.Include(x=>x.RefreshTokens).SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
           
            if (user == null)
                throw new BadRequestException("Invalid token");

            return user;
        }

        private RefreshToken rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            revokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;
        }

        private void removeOldRefreshTokens(User user)
        {
            // remove old inactive refresh tokens from user based on TTL in app settings
            user.RefreshTokens.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }


        private void revokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string ipAddress, string reason)
        {
            // recursively traverse the refresh token chain and ensure all descendants are revoked
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken.IsActive)
                    revokeRefreshToken(childToken, ipAddress, reason);
                else
                    revokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
            }
        }

        private void revokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }


        public async Task<string> Register(RegisterDto model)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var testEmailExist = await _userManager.FindByEmailAsync(model.Email);
            if (testEmailExist != null)
            {
                throw new BadRequestException("Email alredy Exist");
            }

            var user = new User()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Genre = model.Genre,
                UserName = model.Email,
                PasswordHash = passwordHash,
            };

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                var passwordResult = await _userManager.AddPasswordAsync(user, user.PasswordHash);

                if (passwordResult.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, model.role.ToUpper());

                    if (roleResult.Succeeded)
                    {
                        try
                        {

                            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            var param = new Dictionary<string, string?>
    {
                                {"token", token },
                                {"email", user.Email }
    };
                            var callback = QueryHelpers.AddQueryString("https://localhost:7069/api/", param);
                            var message = new Email(new string[] { user.Email }, "Email Confirmation token", callback);
                   
                            _emailSender.SendEmail(message);
                        }
                        catch (Exception ex)
                        {

                        }
                    }


                    return "User Was Created";
                }
            }

            throw new BadRequestException("Faild To Create User");

        }



        public async Task<string> ForgetPassword(ForgotPasswordModel model) {

            var user = await _userManager.FindByEmailAsync(model.UserName);

           

            if (user is null)
            {
                throw new BadRequestException( "UserNotFound" );
            }

            try
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var param = new Dictionary<string, string?>
    {
                                
                    {"email", user.Email },
                    {"token", token }
    };
                var callback = QueryHelpers.AddQueryString(model.ClientUrl, param);
                var message = new Email(new string[] { user.Email }, "ResetPassword token", callback);

                _emailSender.SendEmail(message);
                return "check your email";
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Faild Rest");
            }
        }
            

       


    }
}
