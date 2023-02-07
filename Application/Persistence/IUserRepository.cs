using Domain.Entities.identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistence
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserDetailById(string id);
        Task<List<User>> GetUsers();
    }
}
