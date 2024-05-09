using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Repositories.DbImplementations
{
    public class SuggestionRepository : GenericRepository<Suggestion>, ISuggestionRepository
    {
        private readonly BibContext _context;

        public SuggestionRepository(BibContext context) : base(context)
        {
            _context = context;
        }
    }
}
