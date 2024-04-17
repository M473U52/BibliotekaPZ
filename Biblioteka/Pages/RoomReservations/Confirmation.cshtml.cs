using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Models;
using Biblioteka.Areas.Identity.Data;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Biblioteka.Pages.RoomReservations
{
    [Authorize(Roles = "Employee")]
    public class ConfirmationModel : PageModel
    {
        private readonly UserManager<BibUser> _userManager;
        private IRoomReservationRepository _roomReservationRepository;
        private IEmployeeRepository _employeeRepository;

        public ConfirmationModel(UserManager<BibUser> userManager, IRoomReservationRepository roomReservationRepository, IEmployeeRepository employeeRepository)
        {
            _userManager = userManager;
            _roomReservationRepository = roomReservationRepository;
            _employeeRepository = employeeRepository;
        }

        [BindProperty]
        public RoomReservation RoomReservation { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            if (_roomReservationRepository.getAll() == null)
            {
                return NotFound();
            }

            var reservation = _roomReservationRepository.getOne(id);

            if (reservation == null)
            {
                return NotFound();
            }
            else
            {
                RoomReservation = reservation;

            }
            return Page();

        }

        public async Task<IActionResult> OnPost(int id)
        {
            if (_roomReservationRepository.getAll() == null)
            {
                return Page();
            }
            else
            {
                var roomReservation = _roomReservationRepository.getOne(id);

                if (roomReservation != null)
                {

                    roomReservation.Confirmation = true;

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
                                    roomReservation.employee = employee;
                                    RoomReservation = roomReservation;
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
                    else
                        return NotFound();

                    // ModelState.Remove("Borrowing.employee");
                    // ModelState.Remove("Borrowing.book");



                    try
                    {
                        _roomReservationRepository.Update(RoomReservation);

                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ReservationExists(RoomReservation.reservationId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    return RedirectToPage("./IndexEmployees");
                }
            }

            return NotFound();

        }

        bool ReservationExists(int id)
        {
            var isExisted = _roomReservationRepository.getOne(id);

            return isExisted != null ? true : false;
        }
    }
}
