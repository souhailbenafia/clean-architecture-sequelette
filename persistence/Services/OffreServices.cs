
using Application.DTOs.Offre;
using Application.Persistence;
using Application.Responses;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Offre.Validator;
using Application.Exceptions;
using Application.DTOs.Car;
using Application.DTOs.Car.Validator;
using System.Runtime.CompilerServices;
using Domain.Entities.identity;

namespace persistence.Services
{
    public class OffreServices : IOffreServices
    {

        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;


        public OffreServices(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseServicesResponse> CreateOffre(OffreDto offreDto)
        {
            var response = new BaseServicesResponse();
            var validator = new OffreValidator();
            var validationResult = await validator.ValidateAsync(offreDto);
            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var offre = _mapper.Map<Offre>(offreDto);
                offre = await _unitOfWork.offreRepository.Add(offre);
                await _unitOfWork.Save();
                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = offre.Id;
            }
            return response;
        }

        public async Task<BaseServicesResponse> UpdateOffre(OffreDto offreDto)
        {
            var Validator = new OffreValidator();
            var validationResult = await Validator.ValidateAsync(offreDto);
            if (validationResult.IsValid == false)
            {
                throw new Application.Exceptions.ValidationException(validationResult);
            }
            var offre = await _unitOfWork.offreRepository.Get(offreDto.Id);
            if (offre == null) throw new NotFoundException(nameof(Offre), offre.Id);
            _mapper.Map<Offre>(offreDto);
            await _unitOfWork.offreRepository.Update(offre);
            await _unitOfWork.Save();
            var response = new BaseServicesResponse();
            response.Success = true;
            response.Message = "Update Successful";
            response.Id = offre.Id;
            return response;
        }

        public async Task<BaseServicesResponse> DeleteOffre(int id)
        {
            var offre = await _unitOfWork.offreRepository.Get(id);
            if (offre == null)
                throw new NotFoundException(nameof(Offre), id);

            await _unitOfWork.offreRepository.Delete(offre);
            await _unitOfWork.Save();
            var response = new BaseServicesResponse();
            response.Success = true;
            response.Message = "Deleted Successful";
            response.Id = offre.Id;
            return response;
        }

        public async Task<OffreDto> GetOffreById(int id)
        {
            var offre = await _unitOfWork.offreRepository.Get(id);
            if (offre == null)
                throw new NotFoundException(nameof(Offre), id);

            return _mapper.Map<OffreDto>(offre);
        }

        public async Task<IList<OffreDto>> GetOffresByUserId(string userId)
        {
            var offers = await _unitOfWork.offreRepository.GetAllOffreByUserId(userId);
            return _mapper.Map<List<OffreDto>>(offers);
        }

        public async Task<IList<OffreDto>> getOffresByCarId(int carId)
        {
            var offers = await _unitOfWork.offreRepository.GetAllOffreByCarId(carId);
            return _mapper.Map<List<OffreDto>>(offers);
        }


        public async Task<BaseServicesResponse> CreateOffre(OffreDto offreDto, CarDto carDto)
        {
            var response = new BaseServicesResponse();
            var carValidator = new CarValidator();
            var carValidationResult = await carValidator.ValidateAsync(carDto);
            if (carValidationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = carValidationResult.Errors.Select(q => q.ErrorMessage).ToList();
                return response;
            }
            else
            {
            {
                    var car = _mapper.Map<Car>(carDto);
                    car = await _unitOfWork.carRpository.Add(car);
                    await _unitOfWork.Save();
                    if (car != null)
                    {
                        var offre = _mapper.Map<Offre>(offreDto);
                        offre.CarId = car.Id;
                        offre = await _unitOfWork.offreRepository.Add(offre);
                        await _unitOfWork.Save();
                        response.Success = true;
                        response.Message = "Creation Successful";
                        response.Id = offre.Id;
                    }                   
                }
                return response;

            }
        }


        public async Task <BaseServicesResponse> ChangeOffreAvailablite(int offreId)
        {
            try
            {
                var offer= await _unitOfWork.offreRepository.Get(offreId);
                if (offer == null)
                {
                    throw new Exception("failed to fetch");
                }
                offer.Available = !offer.Available;

                await _unitOfWork.offreRepository.Update(offer);

                await _unitOfWork.Save();

                var response = new BaseServicesResponse();
                response.Success = true;
                response.Message = "Update Successful";
                response.Id = offer.Id;
                return response;

            }
            catch(Exception ex)
            {
                var response = new BaseServicesResponse();
                response.Success = false;
                response.Message = "Update Failed";
                response.Id = offreId;
                return response;
            }
        }


        public async Task<BaseServicesResponse> UpdateOffreAndCar(OffreDto offreDto, CarDto carDto)
        {
            var response = new BaseServicesResponse();
            var carValidator = new CarValidator();
            var carValidationResult = await carValidator.ValidateAsync(carDto);
            if (carValidationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "update Failed";
                response.Errors = carValidationResult.Errors.Select(q => q.ErrorMessage).ToList();
                return response;
            }
            else
            {
                {
                    var car = _mapper.Map<Car>(carDto);
                    await _unitOfWork.carRpository.Update(car);
                    await _unitOfWork.Save();
                    if (car != null)
                    {
                        var offre = _mapper.Map<Offre>(offreDto);
                        await _unitOfWork.offreRepository.Update(offre);
                        await _unitOfWork.Save();
                        response.Success = true;
                        response.Message = "Update Successful";
                        response.Id = offre.Id;
                    }
                }
                return response;

            }
        }

        public async Task<IList<Offre>> GetAllOffresAvailable()
        {
            var offers = await _unitOfWork.offreRepository.GetAllOffresAvailable();
            return _mapper.Map<IList<Offre>>(offers);
        }
    }
}
