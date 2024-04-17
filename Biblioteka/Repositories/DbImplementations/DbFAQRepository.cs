using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Repositories.DbImplementations
{
    public class DbFAQRepository : GenericRepository<FAQ>, IFAQRepository
    {
        private BibContext _context;

        public DbFAQRepository(BibContext context) : base(context)
        {
            _context = context;
        }


    }
}

