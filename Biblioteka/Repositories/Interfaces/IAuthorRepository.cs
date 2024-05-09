using Biblioteka.Models;

namespace Biblioteka.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        public Author getOne(object id);
        public Author getOne(string name);
        public void Update(Author autor);
        public void Delete(object id);
        public List<Author> getAll();

        public void Add(Author author);
        public Author GetByMail(string email);
    }
}