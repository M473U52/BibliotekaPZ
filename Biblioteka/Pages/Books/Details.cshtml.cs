﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;
using Microsoft.AspNetCore.Identity;
using Biblioteka.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using PdfSharp.Pdf.IO;
using System.IO;
using PdfSharp.Pdf;
using Biblioteka.Repositories;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using iText.Commons.Actions.Contexts;

namespace Biblioteka.Views.Books
{
    public class DetailsModel : PageModel
    {
        private UserManager<BibUser> _userManager;
        private IBookRepository _bookRepository;
        private IBookOpinionRepository _bookOpinionRepository;
        private IReaderRepository _readerRepository;
        private IAuthorRepository _authorRepository;
        private IEmployeeRepository _employeeRepository;
        private IBorrowingRepository _borrowingRepository;


        public DetailsModel(
            UserManager<BibUser> userManager,
            IBookRepository bookRepository,
            IBookOpinionRepository bookOpinionRepository, IReaderRepository readerRepository, IAuthorRepository authorRepository, IEmployeeRepository employeeRepository, IBorrowingRepository borrowingRepository)
        {
            _userManager = userManager;
            _bookRepository = bookRepository;
            _bookOpinionRepository = bookOpinionRepository;
            _readerRepository = readerRepository;
            _authorRepository = authorRepository;
            _employeeRepository = employeeRepository;
            _borrowingRepository = borrowingRepository; 

        }
        [BindProperty]
        public List<Book_Opinions> Opinions { get; set; } = default!;

        [BindProperty]
        public Book_OpinionsDto User_Opinion { get; set; } = default!;

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel : IValidatableObject
        {
            [Required(ErrorMessage = "Przewidywana data zwrotu książki jest obowiązkowa."),
                Display(Name = "Przewidywana data zwrotu")]
            public DateTime predictedEndDate { get; set; }
            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                if (predictedEndDate <= DateTime.Today)
                {
                    yield return new ValidationResult("Przewidywana data zwrotu nie może być z przeszłości!", new[] { "predictedEndDate" });
                }
            }
        }

        //[BindProperty]
        public BookDto Book { get; set; }
        public Book bookModel { get; set; }
        public bool IsRatingAdded { get; set; }
        public BibUser user;

        public List<Author> Authors { get; set; }

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

                var authors = _authorRepository.getAll().Where(a => a.books.Any(b => b.bookId == id)).ToList();

                if(authors != null)
                {
                    Authors = authors;
                }
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAddBorrowing(int bookId)
        {
            var context = new ValidationContext(this)
            {
                MemberName = nameof(this.Input)
            };
            bool isPredictedEndDateValid = Validator.TryValidateProperty(this.Input, context, new List<ValidationResult>());
            
            if (!isPredictedEndDateValid)
            {
                TempData["Message"] = $"Error/Formularz został wypełniony błędnie.";
                return  RedirectToPage("./Details", new { id = bookId });
            }
            var user = await _userManager.GetUserAsync(User);
            Reader? reader = _readerRepository.GetByMail(user.Email);

            if (reader == null)
            {
                TempData["Message"] = $"Error/Nie można odnaleźć użytkownika w bazie.";
                return RedirectToPage("./Details", new { id = bookId });
            }

            List<Employee> employeeList = _employeeRepository.GetAll();

            if (!employeeList.Any())
            {
                TempData["Message"] = $"Error/Brak pracownika mogącego obsłużyć wypożyczenie.";
                return RedirectToPage("./Details", new { id = bookId });
            }

            Random random = new Random();
            int a = random.Next(0, employeeList.Count);
            Employee randomEmployee = employeeList[a];           
            Book? book = _bookRepository.getOne(bookId);

            if (book == null)
            {
                TempData["Message"] = $"Error/Brak książki o podanym id.";
                return RedirectToPage("./Details", new { id = bookId });
            }

            var borrowing = new Borrowing();
            borrowing.book = book;
            borrowing.employee = randomEmployee;
            borrowing.borrowDate = DateTime.Now;
            borrowing.plannedReturnDate = Input.predictedEndDate;
            borrowing.returnDate = null;
            borrowing.bookLost = false;
            borrowing.Confirmation = false;
            borrowing.IsPaid = false;
            borrowing.IsReturned = false;   
            borrowing.LateFee = 0;

            borrowing.readers.Add(new Reader_Borrowings
            {
                reader = reader,
                borrow = borrowing,
            });

            _borrowingRepository.Add(borrowing);

            book.availableCopys -= 1;
            _bookRepository.Update(book);

            TempData["Message"] = $"Success/Pomyślnie dokonano wypożyczenia książki: \"{book.title}\" " +
                                $"w dniach {DateTime.Now.ToString("dd.MM.yyyy")} - {Input.predictedEndDate.ToString("dd.MM.yyyy")}.";

            return RedirectToPage("../Borrowings/IndexReader");
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

        public FileResult OnGetPreview(int id)
        {
            var book = _bookRepository.getOne(id);
            string fileName = $"{book.title} ebook.pdf";
            string encodedFileName = Uri.EscapeDataString(fileName);

            // Load the original PDF document
            using (MemoryStream inputPdfStream = new MemoryStream(book.ebookData))
            {
                PdfDocument inputDocument = PdfReader.Open(inputPdfStream, PdfDocumentOpenMode.Import);

                // Create a new PDF document
                PdfDocument outputDocument = new PdfDocument();

                // Copy the first five pages from the input document to the output document
                int pageCount = Math.Min(10, inputDocument.PageCount);
                for (int i = 0; i < pageCount; i++)
                {
                    outputDocument.AddPage(inputDocument.Pages[i]);
                }

                // Save the output document to a MemoryStream
                using (MemoryStream outputPdfStream = new MemoryStream())
                {
                    outputDocument.Save(outputPdfStream, false);
                    string contentDisposition = $"inline; filename*=UTF-8''{encodedFileName}";
                    Response.Headers.Add("Content-Disposition", contentDisposition);
                    return File(outputPdfStream.ToArray(), "application/pdf");
                }
            }
        }
        public FileResult OnGetDownloadPDF(int id)
        {
            var book = _bookRepository.getOne(id);
            string fileName = book.title + " ebook.pdf";

            return File(book.ebookData, "application/pdf", fileName);
        }
        public FileResult OnGetDownloadMP3(int id)
        {
            var book = _bookRepository.getOne(id);
            string fileName = book.title + " audiobook.mp3";

            return File(book.audiobookData, "application/mp3", fileName);
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
    }
}