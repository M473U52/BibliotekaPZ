using Biblioteka.Areas.Identity.Data;

namespace Biblioteka.Repositories.Interfaces
{
    public interface IUserRepository
    {
        BibUser? GetOne(string name, string surname, string? email);
        void Delete(BibUser? user);
    }
}
