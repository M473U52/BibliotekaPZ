using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;
using Biblioteka.Context;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Biblioteka.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using SQLitePCL;

namespace Biblioteka.Views.Books
{
    public class IndexModel : PageModel
    {
        private IBookRepository _bookRepository;
        private IGenreRepository _genreRepository;
        private IBookTypeRepository _bookTypeRepository;
        private ITagRepository _tagRepository;
        private IAuthorRepository _authorRepository;
        private IBorrowingRepository _borrowingRepository;
        private readonly UserManager<BibUser> _userManager;
        private readonly BibContext _bibContext;

        public IndexModel(IBookRepository bookRepository,
                          IGenreRepository genreRepository,
                          IBookTypeRepository bookTypeRepository,
                          ITagRepository tagRepository,
                          IAuthorRepository authorRepository,
                          IBorrowingRepository borrowingRepository,
                          UserManager<BibUser> userManager,
                          BibContext bibContext)
        {
            _bookRepository = bookRepository;
            _genreRepository = genreRepository;
            _bookTypeRepository = bookTypeRepository;
            _tagRepository = tagRepository;
            _authorRepository = authorRepository;
            _borrowingRepository = borrowingRepository;
            _userManager = userManager;
            _bibContext = bibContext;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IEnumerable<Book> Book { get; set; }
        public List<Genre> Genres { get; set; }

        public List<Tag> Tags { get; set; }
        public List<BookType> Types { get; set; }
        public List<Author> Author { get; set; }

        public IList<Borrowing> Borrowing { get; set; } = new List<Borrowing>();

        public List<string> GenresBorrowed { get; set; } = new List<string>();

        public List<string> AuthorsBorrowed { get; set; } = new List<string>();

        public List<Book> RecommendedBooks { get; set; } = new List<Book>();


        public async Task OnGet()
        {
            Book = _bookRepository.SearchBooks(SearchTerm);
            Genres = _genreRepository.getAll().ToList();
            Types = _bookTypeRepository.getAll().ToList();
            Tags = _tagRepository.getAll().ToList();
            Author = _authorRepository.getAll().ToList();

            var loggedInUserId = _userManager.GetUserId(User);

            if (loggedInUserId != null)
            {
                var user = await _userManager.FindByIdAsync(loggedInUserId);

                if (user != null)
                {
                    string name = user.name;
                    string surname = user.surname;
                    Debug.WriteLine(name);

                    var foundReader = await _bibContext.Reader.FirstOrDefaultAsync(r => r.name == name && r.surname == surname);

                    if (foundReader != null)
                    {
                        var foundBorrowings = await _bibContext.Borrowing
                            .Include(b => b.book)
                            .ThenInclude(ba => ba.genre)
                            .Include(b => b.book)
                            .ThenInclude(ba => ba.authors)
                            .ThenInclude(bookAuthor => bookAuthor.author)
                            .Where(b => b.readers.Any(rb => rb.readerId == foundReader.readerId))
                            .Include(b => b.employee)
                            .ToListAsync();

                        Borrowing = foundBorrowings;

                        foreach (var borrowing in foundBorrowings)
                        {
                            if (borrowing.book != null && borrowing.book.genre != null)
                            {
                                GenresBorrowed.Add(borrowing.book.genre.name);
                            }

                            if (borrowing.book != null && borrowing.book.authors != null)
                            {
                                foreach (var bookAuthor in borrowing.book.authors)
                                {
                                    if (bookAuthor.author != null)
                                    {
                                        string fullName = bookAuthor.author.FullName;
                                        AuthorsBorrowed.Add(fullName);
                                        Debug.WriteLine(fullName);
                                    }
                                }
                            }
                        }

                        var recommendedBooksByGenreAndAuthor = new List<Book>();

                        // Szukamy jednej polecanej ksi¹¿ki na podstawie gatunku
                        if (GenresBorrowed.Any())
                        {
                            var randomGenre = GenresBorrowed[new Random().Next(GenresBorrowed.Count)];
                            var randomBookByGenre = _bookRepository.GetRandomBookByGenre(randomGenre);
                            if (randomBookByGenre != null && !Borrowing.Any(b => b.book.bookId == randomBookByGenre.bookId))
                            {
                                recommendedBooksByGenreAndAuthor.Add(randomBookByGenre);
                            }
                        }

                        // Szukamy jednej polecanej ksi¹¿ki na podstawie autora
                        if (AuthorsBorrowed.Any())
                        {
                            var randomAuthor = AuthorsBorrowed[new Random().Next(AuthorsBorrowed.Count)];
                            var randomBookByAuthor = _bookRepository.GetRandomBookByAuthor(randomAuthor);
                            if (randomBookByAuthor != null && !Borrowing.Any(b => b.book.bookId == randomBookByAuthor.bookId))
                            {
                                recommendedBooksByGenreAndAuthor.Add(randomBookByAuthor);
                            }
                        }

                        var borrowedBookIds = Borrowing.Select(br => br.book.bookId).ToList();

                        var highestRatedBooks = await _bibContext.Book
                            .Where(b => b.ratingAVG.HasValue && !borrowedBookIds.Contains(b.bookId))
                            .OrderByDescending(b => b.ratingAVG)
                            .ToListAsync();

                        if (highestRatedBooks.Any())
                        {
                            var randomHighestRatedBook = highestRatedBooks[new Random().Next(highestRatedBooks.Count)];
                            recommendedBooksByGenreAndAuthor.Add(randomHighestRatedBook);
                        }

                        // Dodajemy polecane ksi¹¿ki do listy RecommendedBooks
                        RecommendedBooks.AddRange(recommendedBooksByGenreAndAuthor);
                    }
                    else
                    {
                        Borrowing = new List<Borrowing>();
                    }
                }
            }
        }
        public FileResult OnGetCoverImage(int id)
        {
            var book = _bookRepository.getOne(id);
            if (book == null || book.imageData == null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/okladka.jpg");
                var imageBytes = System.IO.File.ReadAllBytes(filePath);
                return File(imageBytes, "image/jpeg");
            }

            return File(book.imageData, "image/jpeg");
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