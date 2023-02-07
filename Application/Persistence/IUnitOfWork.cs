using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository userRepository { get; }
      public   Task Save();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
