using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Repositories.DbImplementations
{
    public class ReaderEventsRepository : GenericRepository<Reader_Events>, IReaderEventsRepository
    {
        public ReaderEventsRepository(BibContext context) : base(context)
        {
        }

    }
}
