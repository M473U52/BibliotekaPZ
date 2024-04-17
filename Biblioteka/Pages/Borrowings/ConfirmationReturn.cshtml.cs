using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Models;
using Biblioteka.Areas.Identity.Data;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Biblioteka.Pages.Borrowings
{
    [Authorize(Roles = "Employee")]
    public class ConfirmationReturnModel : PageModel
    {
        private IBorrowingRepository _borrowingRepository;
        private readonly UserManager<BibUser> _userManager;
        private IReader_BorrowingsRepository _readerBorrowingRepository;
        private IReaderRepository _readerRepository;
        private IEmployeeRepository _employeeRepository;

        public ConfirmationReturnModel(UserManager<BibUser> userManager, 
            IBorrowingRepository borrowingRepository, IReader_BorrowingsRepository readerBorrowingRepository, IEmployeeRepository employeeRepository, 
            IReaderRepository readerRepository)
        {
            _borrowingRepository = borrowingRepository;
            _userManager = userManager;
            _readerBorrowingRepository = readerBorrowingRepository;
            _readerRepository = readerRepository;
            _employeeRepository = employeeRepository;
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;
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
                Borrowing = borrowing;
                var br = _readerBorrowingRepository.getOne(borrowing.borrowId);
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
            if (_borrowingRepository.GetAll() == null)
            {
                return Page();
            }
            else
            {
                var borrowing = _borrowingRepository.GetOne(id);

                if (borrowing != null && borrowing?.book != null)
                {
                    borrowing.IsReturned = true;
                    borrowing.book.availableCopys += 1;
                    borrowing.returnDate = DateTime.Now;

                    // Przypisz opłatę za zwrot po ustalonym terminie
                    borrowing.LateFee = _borrowingRepository.CalculateLateFee(Borrowing.borrowId);



                    var userId = _userManager.GetUserId(HttpContext.User);

                    if (userId != null)
                    {
                        var user = await _userManager.FindByIdAsync(userId);

                        if (user != null)
                        {
                            string? email = user.Email;

                            bool isEmployee = await _userManager.IsInRoleAsync(user, "Employee");

                            if (isEmployee)
                            {
                                Employee? employee = _employeeRepository.GetByMail(email);
                                if (employee != null)
                                {
                                    if (borrowing.employeeConfirmingReturn == null)
                                    {
                                        borrowing.employeeConfirmingReturn = new EmployeeConfirmingReturn() { employee = employee };
                                    }

                                    _borrowingRepository.AddECR(borrowing);
                                    Borrowing = borrowing;
                                }
                                else
                                    return NotFound();
                            }
                            else
                                return NotFound();
                        }
                        else
                            return NotFound();
                    }

                    try
                    {
                        Borrowing.LateFee = borrowing.LateFee;

                        _borrowingRepository.Update(Borrowing);
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

                return NotFound();
            }
        }


        bool BorrowingExists(int id)
        {
            var isExisted = _borrowingRepository.GetOne(id);
            return isExisted != null ? true : false;
        }


    }
}
