using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Repositories.DbImplementations
{
    public class SuggestionRepository : GenericRepository<Suggestion>, ISuggestionRepository
    {
        private BibContext _context;

        public SuggestionRepository(BibContext context) : base(context)
        {
            _context = context;
        }
        public Suggestion search(string title)
        {
            return _context.Suggestion.FirstOrDefault(p => p.title.ToLower().Contains(title.ToLower()));
        }
    }
}
