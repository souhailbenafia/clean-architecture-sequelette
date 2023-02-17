using Application.DTOs.auth;
using Application.DTOs.Auth;
using Application.Infrastructure;
using Application.Models;
using Application.Services.IServices;
using AutoMapper;
using Domain.Entities.identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net.Mail;
using System.Net;
using static persistence.Services.AuthServices;
using Application.DTOs.User;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {


        private readonly IAuthService _authService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private IdentityOptions Options { get; }

        public AuthenticationController(IAuthService authService, Microsoft.AspNetCore.Identity.UserManager<User> userManager, IMapper mapper, IEmailSender emailSender, IOptions<IdentityOptions> options)
        {
            _authService = authService;
            _mapper = mapper;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] Object model)
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _authService.RefreshToken(refreshToken, ipAddress());
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(LoginDto model)
        {

            try
            {
                var response = await _authService.AuthenticateAsync(model, ipAddress());
                setTokenCookie(response.RefreshToken);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            try
            {
                var response = await _authService.Register(model);
                return Ok(new { message = response });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [AllowAnonymous]
        [HttpPost("sendEmail")]
        public async Task<IActionResult> sendEmail(String emailo)
        {
            try
            {
                var email = new Email(new List<string>() { emailo }, "Email Confirmation", $"hello from the other side");
                _emailSender.SendEmail(email);
                return Ok("email");
            }
            catch (Exception ex)
            {
                return Ok("no");
            }
        }


        [HttpPost("revoke-token")]
        public IActionResult RevokeToken(RevokeTokenRequest model)
        {
            // accept refresh token in request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            _authService.RevokeToken(token, ipAddress());
            return Ok(new { message = "Token revoked" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _authService.GetAll();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("get-user-by-id/{id}")]
        public IActionResult GetById(string id)
        {
            var user = _authService.GetById(id);

            var profil = _mapper.Map<EditProfileDto>(user);
            return Ok(user);
        }

        [HttpGet("{id}/refresh-tokens")]
        public IActionResult GetRefreshTokens(string id)
        {
            var user = _authService.GetById(id);
            return Ok(user.RefreshTokens);
        }

        [HttpGet("EmailConfirmation")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Invalid Email Confirmation Request");
            var confirmResult = await _userManager.ConfirmEmailAsync(user, token);
            if (!confirmResult.Succeeded)
                return BadRequest("Invalid Email Confirmation Request");
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            var response = await _authService.ForgetPassword(model);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordBase resetPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
                return BadRequest("Invalid Request");
            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }
            return Ok();
        }

        [HttpPost("editProfile")]
        public async Task<IActionResult> EditProfile([FromBody] EditProfileDto editProfileDto)
        {
            var user = await _userManager.FindByIdAsync(editProfileDto.Id);
            if (user == null)
                return BadRequest("Invalid Request");

            if (editProfileDto.FirstName != user.FirstName)
            {
                user.FirstName = editProfileDto.FirstName;
            }
            if (editProfileDto.Genre != user.Genre)
            {
                user.Genre = editProfileDto.Genre;
            }
            if (editProfileDto.LastName != user.LastName)
            {
                user.LastName = editProfileDto.LastName;
            }
            if (editProfileDto.ImageUrl != user.ImageUrl)
            {
                user.ImageUrl = editProfileDto.ImageUrl;
            }
            if (editProfileDto.BirthDate != user.BirthDate)
            {
                user.BirthDate = editProfileDto.BirthDate;
            }
            if (editProfileDto.PhoneNumber != user.PhoneNumber)
            {
                user.PhoneNumber = editProfileDto.PhoneNumber;
            }
            var editProfileResult = await _userManager.UpdateAsync(user);
            if (!editProfileResult.Succeeded)
            {
                var errors = editProfileResult.Errors.ToList();
                return BadRequest(new { Errors = errors });
            }
            var profil = _mapper.Map<EditProfileDto>(user);
            return Ok(profil);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(resetPasswordDto.Id);
            if (user == null)
                return BadRequest("Invalid Request");
            var resetPassResult = await _userManager.ChangePasswordAsync(user, resetPasswordDto.OldPassword, resetPasswordDto.Password);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }
            return Ok();
        }

        // helper methods
        private void setTokenCookie(string token)
        {
            // append cookie with refresh token to the http response
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
        private string ipAddress()
        {
            // get source ip address for the current request
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }


    }
}
