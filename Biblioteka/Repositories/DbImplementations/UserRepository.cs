using Biblioteka.Areas.Identity.Data;
using Biblioteka.Context;
using Biblioteka.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Repositories.DbImplementations
{
    public class UserRepository: IUserRepository
    {
        private BibContext _context;
        public UserRepository(BibContext context) 
        {
            _context = context;
        }

        public BibUser? GetOne(string name, string surname, string? email)
        {
            return _context.Users.FirstOrDefault(u => u.name == name && u.surname == surname && u.Email == email);
        }
        public void Delete(BibUser? user) 
        {
            if (user == null) throw new ArgumentNullException();
            else
            {
                _context.Users.Remove(user);
            }
        }
    }
}
