using Application.DTOs.Car;
using Application.DTOs.Offre;
using Application.DTOs.Rent;
using Application.DTOs.Suggestion;
using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class MappingProfile :Profile
    {
        public MappingProfile() {

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, SellerDto>().ReverseMap();
            CreateMap<User, CustumerDto>().ReverseMap();

            CreateMap<User, EditProfileDto>().ReverseMap();

            CreateMap<Rent, RentDto>().ReverseMap();
            CreateMap<Suggestion, SuggestionDto>().ReverseMap();
            CreateMap<Offre, OffreDto>().ReverseMap();
            CreateMap<Car, CarDto>().ReverseMap();
            
        }
    }
}
