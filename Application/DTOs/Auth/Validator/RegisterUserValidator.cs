using Application.DTOs.auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.Validator
{
    public class RegisterUserValidator : AbstractValidator<RegisterDto>
    {
        public RegisterUserValidator()
        {
            RuleFor(p => p.Email)
             .NotEmpty().WithMessage("email is required.")
             .NotNull();
            RuleFor(p => p.FirstName)
             .NotEmpty().WithMessage("ttttttttttttt is required.")
             .NotNull();
            RuleFor(p => p.LastName)
             .NotEmpty().WithMessage("ttttttttttttttttt is required.")
             .NotNull();
            RuleFor(p => p.Password)
             .NotEmpty().WithMessage("ttttttttttttttttt is required.")
             .NotNull();
            RuleFor(p => p.ConfirmePassword)
             .NotEmpty().WithMessage("ttttttttttttttttt is required.")
             .NotNull();

        }
    }
}
