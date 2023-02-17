using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Suggestion.Validator.cs
{
    public class SuggestionValidator : AbstractValidator<SuggestionDto>
    {
        public SuggestionValidator()
        {
            RuleFor(p => p.staus)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();
            RuleFor(p => p.OffreId)
             .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.CustomerId)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.StartDate)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.EndDate)
             .NotEmpty().WithMessage("{PropertyName} is required.");

           
        }
    }
}
