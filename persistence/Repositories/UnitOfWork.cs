using Application.Persistence;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence.Repositories
{
    public class UnitOfWork  : IUnitOfWork
    {

        private UserRepository _UserRepository;
        private CarRepository  _CarRepository;
        private OffreRepository _offreRepository;
        private SuggestionRepository _suggestionRepository;
        private RefreshTokenRepository _refreshTokenRepository;


        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UnitOfWork(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            this._httpContextAccessor = httpContextAccessor;
        }

        public IUserRepository userRepository => _UserRepository ??= new UserRepository(_context);

        public ICarRpository carRpository => _CarRepository ??= new CarRepository(_context);

        public IOffreRepository offreRepository =>_offreRepository ??= new OffreRepository(_context);

        public ISuggestionRepository SuggestionRepository => _suggestionRepository ??= new SuggestionRepository(_context);
        public IRentRepository rentRepository => throw new NotImplementedException();

        public IRefreshTokenRepository refreshTokenRepositoryrefreshTokenRepository => _refreshTokenRepository ??= new RefreshTokenRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
           

            await _context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
