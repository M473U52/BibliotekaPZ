using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace Biblioteka.Pages.Borrowings
{
    [Authorize(Roles = "Employee")]
    public class ConfirmationModel : PageModel
    {

        private IBorrowingRepository _borrowingRepository;
        private IEmployeeRepository _employeeRepository;
        private IReaderRepository _readerRepository;
        private IReader_BorrowingsRepository _readerBorrowingsRepository;
        private readonly UserManager<BibUser> _userManager;

        public ConfirmationModel(UserManager<BibUser> userManager, IBorrowingRepository borrowingRepository,
            IReaderRepository readerRepository, IEmployeeRepository employeeRepository, IReader_BorrowingsRepository readerBorrowingsRepository)
        {

            _borrowingRepository = borrowingRepository;
            _userManager = userManager;
            _readerRepository = readerRepository;
            _employeeRepository = employeeRepository;
            _readerBorrowingsRepository = readerBorrowingsRepository;
        }

        [BindProperty]
        public BorrowingDto Borrowing { get; set; } = default!;
        public Reader Reader { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (_borrowingRepository.GetAll() == null)
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
                    var reader = _readerRepository.getOne(br.readerId);
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
            Debug.WriteLine("Application");
            Debug.WriteLine("Application");
            if (_borrowingRepository.GetAll() == null)
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

                if (existingborrowing != null && existingborrowing?.book != null)
                {

                    existingborrowing.Confirmation = true;

                    existingborrowing.book.availableCopys -= 1;

                    var userId = _userManager.GetUserId(HttpContext.User);

                    if (userId != null)
                    {
                        var user = await _userManager.FindByIdAsync(userId);

                        if (user != null)
                        {
                            string? email = user.Email;
                            Debug.WriteLine("Email: " + email);
                            bool isEmployee = await _userManager.IsInRoleAsync(user, "Employee");

                            if (isEmployee)
                            {
                                Employee? employee = _employeeRepository.GetByMail(email);
                                if (employee != null)
                                {
                                    existingborrowing.employee = employee;
                                    //Borrowing = existingborrowing; ?
                                }
                                else
                                {
                                    Debug.WriteLine("employee == null");
                                    return NotFound();
                                }

                            }
                            else
                            {
                                Debug.WriteLine("isEmployee == false");
                                return NotFound();
                            }


                        }
                        else
                        {
                            Debug.WriteLine("user == null");
                            return NotFound();
                        }
                    }
                    else
                    {
                        Debug.WriteLine("userId== null");
                        return NotFound();
                    }

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

                    return RedirectToPage("./IndexAdmin");
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
