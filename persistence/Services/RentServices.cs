using Application.DTOs.Car;
using Application.DTOs.Car.Validator;
using Application.DTOs.Rent;
using Application.DTOs.Rent.Validator;
using Application.Exceptions;
using Application.Persistence;
using Application.Responses;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence.Services
{
    public class RentServices : IRentServices
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RentServices( IMapper mapper , IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
                
        }
        public async Task<BaseServicesResponse> CreateRent(RentDto rentDto)
        {
            var response = new BaseServicesResponse();
            var validator = new RentValidato();
            var validationResult = await validator.ValidateAsync(rentDto);
            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var rent = _mapper.Map<Rent>(rentDto);
                rent = await _unitOfWork.rentRepository.Add(rent);
                await _unitOfWork.Save();
                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = rent.Id;
            }
            return response;

        }
        public async  Task<BaseServicesResponse> UpdateRent(RentDto rentDto)
        {
            var Validator = new RentValidato();
            var validationResult = await Validator.ValidateAsync(rentDto);
            if (validationResult.IsValid == false)
            {
                throw new Application.Exceptions.ValidationException(validationResult);
            }
            var rent = await _unitOfWork.rentRepository.Get(rentDto.Id);
            if (rent == null) throw new NotFoundException(nameof(Rent), rent.Id);
            _mapper.Map<Rent>(rentDto);
            await _unitOfWork.rentRepository.Update(rent);
            await _unitOfWork.Save();
            var response = new BaseServicesResponse();
            response.Success = true;
            response.Message = "Update Successful";
            response.Id = rent.Id;
            return response;
            
        }

        public async Task<BaseServicesResponse> DeleteRent(int id)
        {
            var rent = await _unitOfWork.rentRepository.Get(id);
            if (rent == null)
                throw new NotFoundException(nameof(Rent), id);

            await _unitOfWork.rentRepository.Delete(rent);
            await _unitOfWork.Save();
            var response = new BaseServicesResponse();
            response.Success = true;
            response.Message = "Deleted Successful";
            response.Id = rent.Id;
            return response;
        }

        public async Task<RentDto> GetRentById(int id)
        {
            var rent = await _unitOfWork.rentRepository.Get(id);
            if (rent == null)
                throw new NotFoundException(nameof(Rent), id);

            return _mapper.Map<RentDto>(rent);
        }



        public Task<IList<RentDto>> GetUserRents(string userId)
        {
            throw new NotImplementedException();
        }

    }
}
