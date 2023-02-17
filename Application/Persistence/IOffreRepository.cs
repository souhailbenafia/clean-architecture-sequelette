using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistence
{
    public interface IOffreRepository : IGenericRepository<Offre>
    {
        Task<IList<Offre>> GetAllOffreByCarId(int id);
         Task<IList<Offre>> GetAllOffreByUserId(string id);
        Task<IList<Offre>> GetAllOffresAvailable();
    }
}
