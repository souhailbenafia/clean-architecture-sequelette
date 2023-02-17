using Application.DTOs.Rent;
using Application.DTOs.Suggestion;
using Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ISuggestionServices
    {
        Task<SuggestionDto> GetSuggestionById(int id);
        Task<BaseServicesResponse> UpdateSuggestion(SuggestionDto suggestionDto);
        Task<BaseServicesResponse> CreateSuggestion(SuggestionDto suggestionDto);
        Task<BaseServicesResponse> DeleteSuggestion(int id);
        Task<IList<SuggestionDto>> GetUserSuggestions(string userId);
    }
}
