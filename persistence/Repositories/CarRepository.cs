using Application.Persistence;
using Application.Responses;
using Domain.Entities;
using Domain.Entities.identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence.Repositories
{
    public class CarRepository : GenericRepository<Car>,ICarRpository
    {
        private readonly AppDbContext _appDbContext;

        public CarRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IList<Car>> GetAllCarByUserId(string id)
        {
            return await _appDbContext.Set<Car>()
                .Include(s => s.Seller).Where(s => s.SellerId.Equals(id))
                .Include(s=> s.Offres)
                .ToListAsync<Car>(); 
        }

    

        public Task<BaseServicesResponse> getOffresByCarId(string carId)
        {
            throw new NotImplementedException();
        }
    }
}
