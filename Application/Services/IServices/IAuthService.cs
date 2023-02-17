using Application.DTOs.auth;
using Application.DTOs.Auth;
using Domain.Entities.identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.IServices
{
    public interface IAuthService
    {
          Task<AuthenticateResponse> AuthenticateAsync(LoginDto model, string ipAddress);
          Task<AuthenticateResponse> RefreshToken(string token, string ipAddress);
          Task<string> ForgetPassword(ForgotPasswordModel model);

             Task<string> Register(RegisterDto model);
            void RevokeToken(string token, string ipAddress);
            IEnumerable<User> GetAll();
            User GetById(string id);
        
    }
}
