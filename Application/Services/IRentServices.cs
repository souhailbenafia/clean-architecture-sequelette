using Application.DTOs.Car;
using Application.DTOs.Rent;
using Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IRentServices
    {
        Task<RentDto> GetRentById(int id);
        Task<BaseServicesResponse> UpdateRent(RentDto rentDto);
        Task<BaseServicesResponse> CreateRent(RentDto rentDto);
        Task<BaseServicesResponse> DeleteRent(int id);
        Task<IList<RentDto>> GetUserRents(string userId);
    }
}
