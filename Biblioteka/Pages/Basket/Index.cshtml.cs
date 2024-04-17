using Biblioteka.Areas.Identity.Data;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Biblioteka.Pages.Basket
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<BibUser> _userManager;
        private IBookRepository _bookRepository;
        private IEmployeeRepository _employeeRepository;
        private IBorrowingRepository _borrowingRepository;
        private IReader_BorrowingsRepository _reader_BorrowingsRepository;
        private IReaderRepository _readerRepository;

        public IndexModel(IBorrowingRepository borrowingRepository, IBookRepository bookRepository, IEmployeeRepository employeeRepository, IReader_BorrowingsRepository reader_BorrowingsRepository, UserManager<BibUser> userManager,  IReaderRepository readerRepository)
        {
            _borrowingRepository = borrowingRepository;
            _bookRepository = bookRepository;
            _employeeRepository = employeeRepository;
            _userManager = userManager;
            _reader_BorrowingsRepository = reader_BorrowingsRepository;
            _readerRepository = readerRepository;
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;
        [BindProperty]
        public DateTime newDate { get; set; } = DateTime.Today;

        public void OnGet()
        {
        }

        public async Task<JsonResult> OnPostFinalizeAsync([FromBody] List<Borrowing> basket)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);

            Reader reader = _readerRepository.GetByMail(user.Email);

            var userBorrowingCount = _reader_BorrowingsRepository.getAll()
            .Count(borrowing => borrowing.readerId == reader.readerId);
            List<Employee> employee = _employeeRepository.GetAll();
            Random random = new Random();
            int a = random.Next(0, employee.Count);
            Employee randomEmployee = employee[a];
            var borDate = newDate;
            var leftBorrowings = 5 - userBorrowingCount;
            foreach (var borrowing in basket)
            {
                if (leftBorrowings > 0)
                {
                    borrowing.borrowDate = borDate;
                    borrowing.employee = randomEmployee;
                    Book book = _bookRepository.getOne(borrowing.book.bookId);
                    borrowing.book = book;

                    //ModelState.Remove("Borrowing.book");
                   // ModelState.Remove("Borrowing.employee");


                    //poprawne dane do bazy
                    //wymusza date miesiac pozniej
                    borrowing.plannedReturnDate = borrowing.borrowDate.AddMonths(1);
                    //realna data zwrotu na null
                    borrowing.returnDate = null;

                    //polaczenie readera z borrowingiem
                    //if (currentUser == null) currentUser = "1";
                    borrowing.readers.Add(new Reader_Borrowings
                    {
                        reader = reader,
                        borrow = borrowing,
                    });

                    _borrowingRepository.Add(borrowing);
                    book.availableCopys -= 1;
                    _bookRepository.Update(book);

                    leftBorrowings -= 1;
                }
                else
                {
                    return new JsonResult(new { success = false });
                }

            }
            return new JsonResult(new { success = true });
        }
    }
}
