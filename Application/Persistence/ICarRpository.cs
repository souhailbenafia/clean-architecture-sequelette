using Application.Responses;
using Domain.Entities;
using Domain.Entities.identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistence
{
    public interface ICarRpository : IGenericRepository<Car>
    {
        Task<IList<Car>> GetAllCarByUserId(string id);
      

    }
}
