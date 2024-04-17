using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Models;
using Biblioteka.Repositories;
using Biblioteka.Repositories.DbImplementations;
using Microsoft.AspNetCore.Authorization;

namespace Biblioteka.Pages.Borrowings
{
    [Authorize(Roles = "Reader")]
    public class BookLostModel : PageModel
    {

        private BorrowingRepository _borrowingRepository;
        private ReaderRepository _readerRepository;
        private Reader_BorrowingsRepository _readerBorrowingsRepository;

        public BookLostModel(ReaderRepository readerRepository, BorrowingRepository borrowingrRepository, Reader_BorrowingsRepository readerBorrowingsRepository)
        {

            _borrowingRepository = borrowingrRepository;
            _readerRepository = readerRepository;
            _readerBorrowingsRepository = readerBorrowingsRepository;
        }

        [BindProperty]
        public BorrowingDto Borrowing { get; set; } = default!;
        public Reader Reader { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (_borrowingRepository.getAll() == null)
            {
                return NotFound();
            }

            var borrowing = _borrowingRepository.GetOne(id);

            if (borrowing == null && borrowing?.book == null)
            {
                return NotFound();
            }
            else
            {
                Borrowing = new BorrowingDto
                {
                    borrowId = borrowing.borrowId,
                    borrowDate = borrowing.borrowDate,
                    plannedReturnDate = borrowing.plannedReturnDate,
                    Confirmation = borrowing.Confirmation,
                    IsReturned = borrowing.IsReturned,
                    IsPaid = borrowing.IsPaid,
                    returnDate = borrowing.returnDate,
                    LateFee = borrowing.LateFee,
                    bookLost = borrowing.bookLost,
                };
                var br = _readerBorrowingsRepository.getOne(borrowing.borrowId);
                if (br != null)
                {
                    var reader = _readerRepository.getOne(br.reader.readerId);
                    if (reader != null)
                        Reader = reader;
                    else
                        return NotFound();
                }
                else
                    return NotFound();

            }
            return Page();

        }

        public async Task<IActionResult> OnPost(int id)
        {
            if (_borrowingRepository.getAll() == null)
            {
                return Page();
            }
            else
            {
                var existingborrowing = _borrowingRepository.GetOne(id);
                existingborrowing.borrowDate = Borrowing.borrowDate;
                existingborrowing.plannedReturnDate = Borrowing.plannedReturnDate;
                existingborrowing.Confirmation = Borrowing.Confirmation;
                existingborrowing.IsReturned = Borrowing.IsReturned;
                existingborrowing.IsPaid = Borrowing.IsPaid;
                existingborrowing.returnDate = Borrowing.returnDate;
                existingborrowing.LateFee = Borrowing.LateFee;
                existingborrowing.bookLost = Borrowing.bookLost;
                if (existingborrowing != null && existingborrowing?.book != null && existingborrowing.Confirmation == true)
                {
                    existingborrowing.bookLost = true;
                    existingborrowing.IsReturned = true;
                    //Borrowing = borrowing; ?

                    //ModelState.Remove("Borrowing.employee");
                    //ModelState.Remove("Borrowing.book");

                    try
                    {
                        _borrowingRepository.Update(existingborrowing);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BorrowingExists(Borrowing.borrowId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToPage("./IndexReader");
                }
            }
            return NotFound();
        }

        bool BorrowingExists(int id)
        {
            var isExisted = _borrowingRepository.GetOne(id);

            return isExisted != null ? true : false;
        }
    }
}
