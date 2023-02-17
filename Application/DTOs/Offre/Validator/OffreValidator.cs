using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Offre.Validator
{
    public  class OffreValidator : AbstractValidator<OffreDto>
    {

        public OffreValidator()
         {
            RuleFor(p => p.Description)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();
            RuleFor(p => p.Name)
             .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.price)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Available)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.CarId)
             .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.sellerId).NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
