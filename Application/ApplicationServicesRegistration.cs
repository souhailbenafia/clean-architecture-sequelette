using Application.DTOs.auth;
using Application.DTOs.Auth.Validator;
using Application.DTOs.Car;
using Application.DTOs.Car.Validator;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ComfigureApplicationServices (this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly ());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IValidator<RegisterDto>, RegisterUserValidator>();
            services.AddScoped<IValidator<CarDto>, CarValidator>();
            return services;
        }
       
    }
}
