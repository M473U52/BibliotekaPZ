using Biblioteka.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Repositories.Interfaces
{
    public interface IBookRepository
    {

        public Book getOne(int id);
        public Book getOne(string title);
        public List<Book> getAll();
        public Book search(string title);
        public void Add(Book book);
        public void Delete(object id);
        public void Update(Book book);
        public IEnumerable<Book> SearchBooks(string searchTerm);
        public List<Book> GetBooksByGenre(string genreName);
        public Book GetRandomBookByGenre(string genreName);
        public Book GetRandomBookByAuthor(string authorFullName);

    }
}