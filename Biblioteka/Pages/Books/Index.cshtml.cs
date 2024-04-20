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

        public List<string> AuthorsBorrowed { get; set;} = new List<string>();


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
                            // Check if the book and genre are not null before adding
                            if (borrowing.book != null && borrowing.book.genre != null)
                            {
                                GenresBorrowed.Add(borrowing.book.genre.name);
                                Debug.WriteLine(borrowing.book.genre.name);
                            }

                            if (borrowing.book != null && borrowing.book.authors != null)
                            {
                                foreach (var bookAuthor in borrowing.book.authors)
                                {
                                    if (bookAuthor.author != null)
                                    {
                                        // Construct the full name using the name, nickname, and surname
                                        string fullName = bookAuthor.author.FullName; // FullName is a computed property you've defined in Author
                                        AuthorsBorrowed.Add(fullName);
                                        Debug.WriteLine(fullName);
                                    }
                                }
                            }


                        }
                        // Count occurrences of each genre
                        var genreNameCounts = GenresBorrowed
    .GroupBy(name => name)
    .Select(group => new { GenreName = group.Key, Count = group.Count() })
    .OrderByDescending(g => g.Count)
    .ToList();
                        Debug.WriteLine(genreNameCounts[0]);
                        Debug.WriteLine(genreNameCounts[1]);
                        Debug.WriteLine(genreNameCounts[2]);
                       


                        // Select the top three most occurred genres, if there are at least three genres
                        List<string> threeMostOccurredGenres = genreNameCounts
                            .Take(3)
                            .Select(g => g.GenreName)
                            .ToList();

                        var authorNameCounts = AuthorsBorrowed
    .GroupBy(fullName => fullName)
    .Select(group => new { FullName = group.Key, Count = group.Count() })
    .OrderByDescending(g => g.Count)
    .ToList();

                        List<string> threeMostOccurredAuthorNames = authorNameCounts
                            .Take(3)
                            .Select(g => g.FullName)
                            .ToList();
                        if (threeMostOccurredGenres.Any())
                        {
                            Debug.WriteLine(threeMostOccurredAuthorNames[0]);
                            Debug.WriteLine(threeMostOccurredAuthorNames[1]);
                            Debug.WriteLine(authorNameCounts[0]);
                            Debug.WriteLine(authorNameCounts[1]);

                        }
                        else
                        {
                            Debug.WriteLine("nie ma:(");
                        }

                        if (threeMostOccurredGenres.Any())
                        {
                        Debug.WriteLine(threeMostOccurredGenres[0]);
                            Debug.WriteLine(threeMostOccurredGenres[1]);
                            Debug.WriteLine(threeMostOccurredGenres[2]);

                        }
                        else
                        {
                            Debug.WriteLine("nie ma:(");
                        }
                    }
                    else
                    {
                        Borrowing = new List<Borrowing>();

                    }
                }
            }
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