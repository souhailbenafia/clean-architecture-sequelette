using Application.Persistence;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence.Repositories
{
    public class SuggestionRepository : GenericRepository<Suggestion>,ISuggestionRepository
    {
        public AppDbContext _appDbContext { get; set; }

        public SuggestionRepository(AppDbContext appDbContext) :base(appDbContext) {
            _appDbContext = appDbContext;
        }
    }
}
