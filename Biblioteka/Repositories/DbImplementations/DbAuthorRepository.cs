using Biblioteka.Models;
using Biblioteka.Context;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Repositories.DbImplementations;
using System.IO;
namespace Biblioteka.Repositories
{
    public class DbAuthorRepository : GenericRepository<Author>, IAuthorRepository
    {

        private readonly BibContext _context;
        public DbAuthorRepository(BibContext context): base(context)
        {
            _context = context;
        }

        public Author GetByMail(string email)
        {
            return _context.Author.FirstOrDefault(author => author.email == email);
            /* foreach (var author in getAll())
             {
                 if (author.email == email)
                     return author;
             }
             return null;*/
        }

        public Author getOne(string name)
        {
            string[] fullName = name.Split(" ");
            if (fullName.Length < 2)
            {
                return null;
            }
            return _context.Author.FirstOrDefault(a => a.name == fullName[0] && a.surname == fullName[1]);
        }
    }
}
