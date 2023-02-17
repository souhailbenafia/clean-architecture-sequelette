using Application.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence.Repositories
{
    public class OffreRepository : GenericRepository<Offre>, IOffreRepository
    {

        private readonly AppDbContext _appDbContext;
        public OffreRepository(AppDbContext appDbContext) : base(appDbContext) 
        {
            _appDbContext = appDbContext;   
        }

        
        public async Task<IList<Offre>> GetAllOffreByCarId(int id)
        {
            return await _appDbContext.Set<Offre>()
                .Include(s => s.Car).Where(s => s.CarId.Equals(id))
                .ToListAsync<Offre>();
        }

        public async Task<IList<Offre>> GetAllOffreByUserId(string id)
        {
            return await _appDbContext.Set<Offre>()
                .Include(s => s.Car).Include(t=>t.Car.Seller).Where(s => s.Car.SellerId.Equals(id))
                .ToListAsync<Offre>();
        }

        public async Task<IList<Offre>> GetAllOffresAvailable()
        {
            return await _appDbContext.Set<Offre>()
                .Include(s => s.Car)
                .Where(o=>o.Available == true)
                .ToListAsync<Offre>();
        }
    }
}
