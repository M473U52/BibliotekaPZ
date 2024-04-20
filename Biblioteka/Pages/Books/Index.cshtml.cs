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

                        if (GenresBorrowed!=null)
                        {

                            var genreNameCounts = GenresBorrowed
                            .GroupBy(name => name)
                            .Select(group => new { GenreName = group.Key, Count = group.Count() })
                            .OrderByDescending(g => g.Count)
                            .ToList();


                            if(genreNameCounts.Count >= 2)
                            {
                                /*List<string> twoMostOccurredGenres = genreNameCounts
                                .Take(2)
                                .Select(g => g.GenreName)
                                .ToList();*/
                                bool isDifferentBook = true;
                                for (int i=0;i<genreNameCounts.Count;i++)
                                {
                                    for(int j=0;j<15;j++)
                                    {
                                        List<Book> booksByMostCommonGenre = _bookRepository.GetBooksByGenre(genreNameCounts[i].GenreName);


                                        //  Debug.WriteLine(booksByMostCommonGenre.Count > 0 ? booksByMostCommonGenre[i].bookId.ToString() : "No books found");

                                        Random rnd = new Random();
                                        int randomIndex = rnd.Next(booksByMostCommonGenre.Count);  // Generate a random index
                                        Book randomBook = booksByMostCommonGenre[randomIndex];  // Retrieve the book at the randomly generated index

                                        foreach (var Book in RecommendedBooks)
                                        {
                                            if (randomBook == Book)
                                            {
                                                isDifferentBook = false;
                                                break;
                                            }

                                        }
                                        if (isDifferentBook)
                                        {
                                            RecommendedBooks.Add(randomBook);
                                            break;

                                        }
                                        
                                        /*if (recommendedBook == )
                                        for(int j = 0; j < 15;j++) {

                                            RecommendedBooks.Add(recommendedBook);
                                        }*/
                                    }

                                }

                                for (int i = 1; i < genreNameCounts.Count; i++)
                                {
                                    for (int j = 0; j < 15; j++)
                                    {
                                        List<Book> booksByMostCommonGenre2 = _bookRepository.GetBooksByGenre(genreNameCounts[i].GenreName);


                                        //  Debug.WriteLine(booksByMostCommonGenre.Count > 0 ? booksByMostCommonGenre[i].bookId.ToString() : "No books found");

                                        Random rnd = new Random();
                                        int randomIndex = rnd.Next(booksByMostCommonGenre2.Count);  // Generate a random index
                                        Book randomBook = booksByMostCommonGenre2[randomIndex];  // Retrieve the book at the randomly generated index

                                        foreach (var Book in RecommendedBooks)
                                        {
                                            if (randomBook == Book)
                                            {
                                                isDifferentBook = false;
                                                break;
                                            }

                                        }
                                        if (isDifferentBook)
                                        {
                                            RecommendedBooks.Add(randomBook);
                                            break;

                                        }

                                        /*if (recommendedBook == )
                                        for(int j = 0; j < 15;j++) {

                                            RecommendedBooks.Add(recommendedBook);
                                        }*/
                                    }

                                }


                             //   List<Book> booksByMostCommonGenre2 = _bookRepository.GetBooksByGenre(genreNameCounts[1].GenreName);
                              //  Debug.WriteLine(booksByMostCommonGenre2.Count > 0 ? booksByMostCommonGenre2[0].bookId.ToString() : "No books found");
                            //    Book recommendedBook2 = booksByMostCommonGenre2[0];
                             //   RecommendedBooks.Add(recommendedBook2);


                            }
             
                            
                        }
                 


                        if (AuthorsBorrowed != null && AuthorsBorrowed.Any())
                        {
                            var authorNameCounts = AuthorsBorrowed
                                .GroupBy(fullName => fullName)
                                .Select(group => new { FullName = group.Key, Count = group.Count() })
                                .OrderByDescending(g => g.Count)
                                .ToList();

                            // Check that we have at least one author before proceeding
                            if (authorNameCounts.Count > 0)
                            {

                                for(int i=0;i<authorNameCounts.Count;i++)
                                {
                                    var authorName = authorNameCounts[i];
                                    bool isDifferentBook = true;

                                    Book recommendedBook3 = _bookRepository.GetRandomBookByAuthor(authorName.FullName);
                                    foreach (var Book in RecommendedBooks)
                                    {
                                        if (recommendedBook3 == Book)
                                        {
                                            isDifferentBook = false;
                                        }
                                    }

                                    if (isDifferentBook)
                                    {
                                        RecommendedBooks.Add(recommendedBook3);
                                        return;

                                    }
                                }
                               
                            }
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