using Application.DTOs.Car;
using Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ICarServices
    {
        Task<CarDto> GetCarById(int id);
        Task<BaseServicesResponse> UpdateCar(CarDto carDto);
        Task<BaseServicesResponse> CreateCar(CarDto carDto);
        Task<BaseServicesResponse> DeleteCar(int id);
        Task<IList<CarDto>> GetUserCars(string userId);
    }
}
