using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;

namespace Biblioteka.Views.Books
{
    public class IndexModel : PageModel
    {
        private IBookRepository _bookRepository;
        private IGenreRepository _genreRepository;
        private IBookTypeRepository _bookTypeRepository;
        private ITagRepository _tagRepository;
        private IAuthorRepository _authorRepository;

        public IndexModel(IBookRepository bookRepository,
                          IGenreRepository genreRepository,
                          IBookTypeRepository bookTypeRepository,
                          ITagRepository tagRepository,
                          IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _genreRepository = genreRepository;
            _bookTypeRepository = bookTypeRepository;
            _tagRepository = tagRepository;
            _authorRepository = authorRepository;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IEnumerable<Book> Book { get; set; }
        public List<Genre> Genres { get; set; }

        public List<Tag> Tags { get; set; }
        public List<BookType> Types { get; set; }
        public List<Author> Author { get; set; }

        public void OnGet()
        {
            Book = _bookRepository.SearchBooks(SearchTerm);
            Genres = _genreRepository.getAll().ToList();
            Types = _bookTypeRepository.getAll().ToList();
            Tags = _tagRepository.getAll().ToList();
            Author = _authorRepository.getAll().ToList();
        }
        public int BookCountForGenre(int genre_id)
        {
            return Book.Where(b => b.genre.genreId == genre_id).Count();
        }
        public int BookCountForType(int type_id)
        {
            return Book.Where(b => b.type.typeId == type_id).Count();
        }
        public int AvailableBookCount()
        {
            return Book.Where(b => b.availableCopys > 0).Count();
        }
        public int LowQuantityBookCount()
        {
            return Book.Where(b => (b.availableCopys < 10 && b.availableCopys > 0)).Count();
        }
        public int NotAvailableBookCount()
        {
            return Book.Where(b => b.availableCopys == 0).Count();
        }
    }

}