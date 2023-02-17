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

        ICarRpository carRpository { get; }
        IOffreRepository offreRepository { get; }

        ISuggestionRepository SuggestionRepository { get; }

        IRentRepository rentRepository { get; }

        IRefreshTokenRepository refreshTokenRepositoryrefreshTokenRepository { get; }


      public   Task Save();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
