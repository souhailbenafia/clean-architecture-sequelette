using Application.DTOs.Car;
using Application.DTOs.Car.Validator;
using Application.Exceptions;
using Application.Persistence;
using Application.Responses;
using Application.Services;
using AutoMapper;
using Azure;
using Azure.Core;
using Domain.Entities;
using MediatR;
using persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence.Services
{
    public class CarServices : ICarServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CarServices(IMapper mapper , IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async  Task<BaseServicesResponse> CreateCar(CarDto carDto)
        {
            var response = new BaseServicesResponse();
            var validator = new CarValidator();
            var validationResult = await validator.ValidateAsync(carDto);
            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var car = _mapper.Map<Car>(carDto);
                car = await _unitOfWork.carRpository.Add(car);
                await _unitOfWork.Save();
                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = car.Id;
            }
            return response;
        }

        public async Task<BaseServicesResponse> UpdateCar(CarDto carDto)
        {
            var Validator = new CarValidator();
            var validationResult = await Validator.ValidateAsync(carDto);
            if (validationResult.IsValid == false)
            {
                throw new Application.Exceptions.ValidationException(validationResult);
            }
            var car = await _unitOfWork.carRpository.Get(carDto.Id);
            if (car == null) throw new NotFoundException(nameof(car), car.Id);
            _mapper.Map<Car>(carDto);
            await _unitOfWork.carRpository.Update(car);
            await _unitOfWork.Save();
            var response = new BaseServicesResponse();
            response.Success = true;
            response.Message = "Update Successful";
            response.Id = car.Id;
            return response;
        }

        public async Task<BaseServicesResponse> DeleteCar(int id)
        {
            var car = await _unitOfWork.carRpository.Get(id);
            if (car == null)
                throw new NotFoundException(nameof(Car), id);

            await _unitOfWork.carRpository.Delete(car);
            await _unitOfWork.Save();
            var response = new BaseServicesResponse();
            response.Success = true;
            response.Message = "Deleted Successful";
            response.Id = car.Id;
            return response;
        }

        public async Task<CarDto> GetCarById(int id)
        {
            var  car = await _unitOfWork.carRpository.Get(id);
            if (car == null)
                throw new NotFoundException(nameof(Car), id);

            return _mapper.Map<CarDto>(car);
        }

        public async  Task<IList<CarDto>> GetUserCars(string userId)
        {
            var cars = await _unitOfWork.carRpository.GetAllCarByUserId(userId);
            return _mapper.Map<List<CarDto>>(cars);
        }

    }
}
