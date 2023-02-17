using Application.DTOs.auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.Validator
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {

        public LoginValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(p => p.Password)
             .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
