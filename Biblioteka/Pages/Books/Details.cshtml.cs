using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;
using Microsoft.AspNetCore.Identity;
using Biblioteka.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Biblioteka.Views.Books
{
    public class DetailsModel : PageModel
    {
        private readonly UserManager<BibUser> _userManager;
        private IBookRepository _bookRepository;
        private IBookOpinionRepository _bookOpinionRepository;
        private IReaderRepository _readerRepository;


        public DetailsModel(UserManager<BibUser> userManager, IBookRepository bookRepository,
            IBookOpinionRepository bookOpinionRepository, IReaderRepository readerRepository)
        {
            _userManager = userManager;
            _bookRepository = bookRepository;
            _bookOpinionRepository = bookOpinionRepository;
            _readerRepository = readerRepository;
        }
        [BindProperty]
        public List<Book_Opinions> Opinions { get; set; } = default!;

        [BindProperty]
        public Book_OpinionsDto User_Opinion { get; set; } = default!;

        //[BindProperty]
        public BookDto Book { get; set; }
        public Book bookModel { get; set; }
        public bool IsRatingAdded { get; set; }
        public BibUser user;

        public IActionResult OnGet(int id)
        {
            var book = _bookRepository.getOne(id);
            bookModel = book;
            Opinions = _bookOpinionRepository.getOpinionsForBook(book);
            
            Reader reader;

            if (User.IsInRole("Reader") && !User.IsInRole("Employee"))
            {
                reader = _readerRepository.GetByMail(User.Identity.Name);

                IsRatingAdded = Opinions.ToList().Where(o => (o.readerId == reader.readerId) && (o.bookId == book.bookId)).Any();
            }
            else
            {
                IsRatingAdded = true;
            }

            if (book == null)
            {
                return NotFound();
            }
            else
            {
                Book = new BookDto
                {
                    bookId = book.bookId,
                    title = book.title,
                    ISBN = book.ISBN,
                    description = book.description,
                    availableCopys = book.availableCopys,
                    ratingAVG = book.ratingAVG,
                    releaseDate = book.releaseDate,
                    floor = book.floor,
                    alley = book.alley,
                    rowNumber = book.rowNumber
                };
            }
            return Page();
        }

        public IActionResult OnPostAddOpinion(int bookId)
        {
            var id = bookId; //Book.bookId;

            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine($"Model error: {error.ErrorMessage}");
                    }

                }
                return OnGet(id);
            }

            //ModelState.Remove("Book.ISBN");
            // ModelState.Remove("Book.genre");
            //ModelState.Remove("Book.publisher");
            //ModelState.Remove("Book.type");
            //ModelState.Remove("Book.title");
            //ModelState.Remove("Book.description");
            //ModelState.Remove("User_Opinion.reader");
            // ModelState.Remove("User_Opinion.book");

            var existingBook = _bookRepository.getOne(id);

            //existingBook.title = Book.title;
            //existingBook.ISBN = Book.ISBN;
            //existingBook.description = Book.description;
            //existingBook.availableCopys = Book.availableCopys;
            //existingBook.ratingAVG = Book.ratingAVG;
            //existingBook.releaseDate = Book.releaseDate;
            //existingBook.floor = Book.floor;
            //existingBook.alley = Book.alley;
            //existingBook.rowNumber = Book.rowNumber;



            var user_opinion = User_Opinion;
            Book_Opinions newUser_Opinion = new Book_Opinions()
            {
                bookId = existingBook.bookId,
                readerId = User_Opinion.readerId,
                rating = User_Opinion.rating,
                addedDate = DateTime.Now,
                opinion = User_Opinion.opinion,
            };

            if (User.IsInRole("Reader") && !User.IsInRole("Employee"))
            {
                newUser_Opinion.reader = _readerRepository.GetByMail(User.Identity.Name);
            }


            newUser_Opinion.book = existingBook;

            var opinionsCount = _bookOpinionRepository.getOpinionsForBook(existingBook).Count;

            if (existingBook.ratingAVG == null)
            {
                existingBook.ratingAVG = Math.Round(User_Opinion.rating, 2);
            }
            else
            {
                double? newRating = ((existingBook.ratingAVG * opinionsCount) + User_Opinion.rating) / (opinionsCount + 1);
                existingBook.ratingAVG = Math.Round((double)newRating, 2);
            }

            User_Opinion.addedDate = DateTime.Now;


            var opinion = newUser_Opinion;
            var booknew = existingBook;

            _bookRepository.Update(booknew);
            _bookOpinionRepository.Add(opinion);


            return RedirectToPage("./Details", new { id = existingBook.bookId });
        }
        public IActionResult OnPostDeleteComment(int bookId)
        {
            var id = bookId; //Book.bookId;
            var book = _bookRepository.getOne(id);

            Reader reader = _readerRepository.GetByMail(User.Identity.Name);

            Book_Opinions opinion = _bookOpinionRepository.getOpinionForReaderAndBook(reader, book);

            var opinionsCount = _bookOpinionRepository.getOpinionsForBook(book).Count;

            if (opinionsCount > 1)
            {
                double? newRating = ((book.ratingAVG * opinionsCount) - opinion.rating) / (opinionsCount - 1);
                book.ratingAVG = Math.Round((double)newRating, 2);
            }
            else
            {
                book.ratingAVG = null;
            }

            _bookRepository.Update(book);


            _bookOpinionRepository.DeleteOpinion(opinion);

            return RedirectToPage("./Details", new { id = book.bookId });
        }

        public FileResult OnGetDownloadPDF(int id)
        {
            var book = _bookRepository.getOne(id);
            string fileName = book.title + " ebook.pdf";

            return File(book.ebookData, "application/pdf", fileName);
        }
    }
}
