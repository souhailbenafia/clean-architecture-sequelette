using Application.DTOs.Car;
using Application.DTOs.Car.Validator;
using Application.DTOs.Rent;
using Application.DTOs.Suggestion;
using Application.DTOs.Suggestion.Validator.cs;
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
    public class SuggestionServices : ISuggestionServices
    {

        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        public SuggestionServices(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
    
        public async Task<BaseServicesResponse> CreateSuggestion(SuggestionDto suggetionDto)
        {
            var response = new BaseServicesResponse();
            var validator = new SuggestionValidator();
            var validationResult = await validator.ValidateAsync(suggetionDto);
            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var suggestion = _mapper.Map<Suggestion>(suggetionDto);
                suggestion = await _unitOfWork.SuggestionRepository.Add(suggestion);
                await _unitOfWork.Save();
                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = suggestion.Id;
            }
            return response;

        }
        public async Task<BaseServicesResponse> UpdateSuggestion(SuggestionDto suggestionDto)
        {
            var Validator = new SuggestionValidator();
            var validationResult = await Validator.ValidateAsync(suggestionDto);
            if (validationResult.IsValid == false)
            {
                throw new Application.Exceptions.ValidationException(validationResult);
            }
            var suggestion = await _unitOfWork.SuggestionRepository.Get(suggestionDto.Id);
            if (suggestion == null) throw new NotFoundException(nameof(Suggestion), suggestion.Id);
            _mapper.Map<Suggestion>(suggestionDto);
            await _unitOfWork.SuggestionRepository.Update(suggestion);
            await _unitOfWork.Save();
            var response = new BaseServicesResponse();
            response.Success = true;
            response.Message = "Update Successful";
            response.Id = suggestion.Id;
            return response;
        }

        public async  Task<BaseServicesResponse> DeleteSuggestion(int id)
        {
            var suggestion = await _unitOfWork.SuggestionRepository.Get(id);
            if (suggestion == null)
                throw new NotFoundException(nameof(Suggestion), id);

            await _unitOfWork.SuggestionRepository.Delete(suggestion);
            await _unitOfWork.Save();
            var response = new BaseServicesResponse();
            response.Success = true;
            response.Message = "Deleted Successful";
            response.Id = suggestion.Id;
            return response;
        }

        public async Task<SuggestionDto> GetSuggestionById(int id)
        {
            var suggestion = await _unitOfWork.SuggestionRepository.Get(id);
            if (suggestion == null)
                throw new NotFoundException(nameof(Suggestion), id);
            return _mapper.Map<SuggestionDto>(suggestion);
        }

        public Task<IList<SuggestionDto>> GetUserSuggestions(string userId)
        {
            throw new NotImplementedException();
        }

    }
}
