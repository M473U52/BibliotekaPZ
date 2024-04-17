using Biblioteka.Models;
using Biblioteka.Context;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Repositories.DbImplementations
{
    public class PositionRepository : GenericRepository<Position>, IPositionRepository
    {
        private readonly BibContext _context;
        public PositionRepository(BibContext context) : base(context)
        {
            _context = context;
        }
    }

}