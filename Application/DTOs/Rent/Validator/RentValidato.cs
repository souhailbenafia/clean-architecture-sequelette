using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Rent.Validator
{
    public class RentValidato : AbstractValidator<RentDto>
    {
        public RentValidato()
        {
            RuleFor(p => p.IsDeleted)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull();
            RuleFor(p => p.IsRentFinished)
             .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.SuggestionId)
            .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
