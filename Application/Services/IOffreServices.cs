using Application.DTOs.Car;
using Application.DTOs.Offre;
using Application.Responses;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IOffreServices
    {

        Task<OffreDto> GetOffreById(int id);
        Task<BaseServicesResponse> UpdateOffre(OffreDto offreDto);
        Task<BaseServicesResponse> CreateOffre(OffreDto offreDto, CarDto carDto);
        Task<BaseServicesResponse> DeleteOffre(int id);
        Task<IList<OffreDto>> GetOffresByUserId(string userId);
        Task<IList<OffreDto>> getOffresByCarId(int carId);
        Task<BaseServicesResponse> ChangeOffreAvailablite(int offreId);
        Task<BaseServicesResponse> UpdateOffreAndCar(OffreDto offreDto, CarDto carDto);
        Task<IList<Offre>> GetAllOffresAvailable();
    }
}
