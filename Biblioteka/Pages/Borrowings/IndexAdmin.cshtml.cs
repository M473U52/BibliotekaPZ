using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace Biblioteka.Pages.Borrowings
{
    [Authorize(Roles = "Employee, Admin")]
    public class IndexAdminModel : PageModel
    {
        private IBorrowingRepository _borrowingRepository;
        private IReaderRepository _readerRepository;
        private IEmployeeRepository _employeeRepository;
        private IReader_BorrowingsRepository _readerBorrowingRepository;
        private IBookRepository _bookRepository;

        public IndexAdminModel(IBorrowingRepository borrowingRepository, IReaderRepository readerRepository, IReader_BorrowingsRepository readerBorrowingRepository, IEmployeeRepository employeeRepository, IBookRepository bookRepository)
        {
            _borrowingRepository = borrowingRepository;
            _readerRepository = readerRepository;
            _readerBorrowingRepository = readerBorrowingRepository;
            _employeeRepository = employeeRepository;
            _bookRepository = bookRepository;

        }

        public IList<Borrowing> Borrowing { get; set; } = default!;

        public IList<Reader> Readers { get; set; } = default!;
        public IList<Reader_Borrowings> Reader_Borrowings { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Borrowing = _borrowingRepository.SearchBorrowings(SearchTerm);
            }
            else
            {
                Borrowing = _borrowingRepository.GetAll();
            }

            Readers = _readerRepository.getAll();
            Reader_Borrowings = _readerBorrowingRepository.getAll();
        }
        public IActionResult OnPostDeleteBorrowing(int borrowingId)
        {
            var borrowing = _borrowingRepository.GetOne(borrowingId);

            if (borrowing != null)
            {
                var book = _bookRepository.getOne(borrowing.book.bookId);
                if (book != null)
                {
                    TempData["Message"] = $"Success/Pomyślnie usunięto wypożyczenie książki \"{book.title}\" " +
                                    $"z dnia {borrowing.borrowDate.ToString("dd.MM.yyyy")}.";

                    _borrowingRepository.Delete(borrowingId);
                    book.availableCopys += 1;
                    _bookRepository.Update(book);
                       
                }
                else
                {
                    TempData["Message"] = $"Error/Brak książki o takim ID.";
                }
            }
            else
            {
                TempData["Message"] = $"Error/Brak wypożyczenia o takim ID.";
            }
            return RedirectToPage("./IndexAdmin", new { searchTerm = "" });
        }
        public IActionResult OnPostConfirmBorrowing(int borrowingId)
        {
            var borrowing = _borrowingRepository.GetOne(borrowingId);
            if (borrowing == null)
            {
                TempData["Message"] = $"Error/Brak wypożyczenia o takim ID.";
            }
            else
            {              
                var employee = _employeeRepository.GetByMail(User.Identity.Name);
                if (employee != null)
                {
                    borrowing.Confirmation = true;
                    borrowing.book.availableCopys -= 1;
                    borrowing.employee = employee;
                    _borrowingRepository.Update(borrowing);
                    TempData["Message"] = $"Success/Pomyślnie potwierdzono odbiór ksiązki \"{borrowing.book.title}\"";
                }
                else
                {
                    TempData["Message"] = $"Error/Nie udało się znaleźć pracownika.";
                }             
               
            }
            return RedirectToPage("./IndexAdmin", new { searchTerm = "" });
        }
        public IActionResult OnPostConfirmReturn(int borrowingId)
        {
            var borrowing = _borrowingRepository.GetOne(borrowingId);

            if (borrowing == null)
            {
                TempData["Message"] = $"Error/Brak wypożyczenia o takim ID.";
            }
            else
            {
                var employee = _employeeRepository.GetByMail(User.Identity.Name);
                if (employee == null)
                {
                    TempData["Message"] = $"Error/Nie udało się znaleźć pracownika.";
                }
                else
                {
                    borrowing.IsReturned = true;
                    borrowing.book.availableCopys += 1;
                    borrowing.returnDate = DateTime.Now;
                    borrowing.LateFee = _borrowingRepository.CalculateLateFee(borrowingId);

                    if (borrowing.employeeConfirmingReturn == null)
                    {
                        borrowing.employeeConfirmingReturn = new EmployeeConfirmingReturn() { employee = employee };
                    }

                    _borrowingRepository.AddECR(borrowing);
                    _borrowingRepository.Update(borrowing);
                    TempData["Message"] = $"Success/Pomyślnie potwierdzono zwrot ksiązki \"{borrowing.book.title}\".";
                }              
            }
            return RedirectToPage("./IndexAdmin", new { searchTerm = "" });
        }
        public IActionResult OnPostConfirmPayment(int borrowingId)
        {
            var borrowing = _borrowingRepository.GetOne(borrowingId);
            if (borrowing == null)
            {
                TempData["Message"] = $"Error/Brak wypożyczenia o takim ID.";
            }
            else
            {
               
                var employee = _employeeRepository.GetByMail(User.Identity.Name);
                if (employee == null)
                {
                    TempData["Message"] = $"Error/Nie udało się znaleźć pracownika.";
                }
                else
                {
                    borrowing.IsPaid = true;
                    borrowing.employeeConfirmingPayment = new EmployeeConfirmingPayment() { employee = employee };
                    _borrowingRepository.AddECP(borrowing);
                    _borrowingRepository.Update(borrowing);
                    TempData["Message"] = $"Success/Pomyślnie potwierdzono opłatę za książkę \"{borrowing.book.title}\".";
                }
            }
            return RedirectToPage("./IndexAdmin", new { searchTerm = "" });
        }
    }
}
