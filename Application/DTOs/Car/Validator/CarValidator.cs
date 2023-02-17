using Application.DTOs.auth;
using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Car.Validator
{
    public class CarValidator : AbstractValidator<CarDto>
    {

        public CarValidator()
        {
        
        
        }
    }
}
