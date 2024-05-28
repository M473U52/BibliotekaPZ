using Biblioteka.Models;
using Biblioteka.Context;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Repositories.DbImplementations;

namespace Biblioteka.Repositories
{
    public class ReaderRepository : GenericRepository<Reader>, IReaderRepository
    {
        private readonly BibContext _context;
        public ReaderRepository(BibContext context) : base(context)
        {
            _context = context;
        }
        public List<Reader> GetAll()
        {
            return _context.Reader
                .ToList();
        }

        public int GetLastId()
        {
            var id = 1;
            var readers = GetAll();
            if (readers.Any())
                id = readers.Max(e => e.readerId) + 1;
            return id;
        }

        public Reader GetByMail(string? email)
        {
            foreach (var reader in GetAll())
            {
                if (reader.email == email)
                    return reader;
            }
            return null;
        }

        public Reader getOneByEmail(string email)
        {
            return _context.Reader.FirstOrDefault(r => r.email == email);
        }
    }
}
