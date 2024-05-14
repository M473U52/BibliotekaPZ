using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Biblioteka.Repositories.DbImplementations;
using Biblioteka.Repositories;

namespace Biblioteka.Pages.RoomReservations
{
    [Authorize(Roles = "Admin, Employe")]
    public class IndexEmployeesModel : PageModel
    {
        private readonly IRoomReservationRepository _roomReservationRepository;

        public IndexEmployeesModel(IRoomReservationRepository roomReservationRepository)
        {
            _roomReservationRepository = roomReservationRepository;
        }

        public IList<RoomReservation> RoomReservation { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public void OnGet(string searchTerm)
        {
            SearchTerm = searchTerm;
            if (SearchTerm == null)
            {
                RoomReservation = _roomReservationRepository.getAll();
            } else
            {
                RoomReservation = _roomReservationRepository.SearchRoomReservations(SearchTerm);
            }

            
        }

        public IActionResult OnPostDeleteBorrowing(int reservationId)
        {
            var reservation = _roomReservationRepository.getOne(reservationId);

            if (reservation != null)
            {
                
                    TempData["Message"] = $"Success/Pomyślnie usunięto wynajęcie sali: \"{reservation.roomId}\" " +
                                    $"z dnia {reservation.begginingOfReservationDate.ToString("dd.MM.yyyy")}.";

                    _roomReservationRepository.Delete(reservationId);
            }
            else
            {
                TempData["Message"] = $"Error/Brak sali o takim ID.";
            }
            return RedirectToPage("./IndexEmployees");
        }
        public IActionResult OnPostConfirmBorrowing(int reservationId)
        {
            var reservation = _roomReservationRepository.getOne(reservationId);
            if (reservation == null)
            {
                TempData["Message"] = $"Error/Brak wynajęcia o takim ID.";
            }
            else
            {

                    reservation.Confirmation = true;
                _roomReservationRepository.Update(reservation);
                    TempData["Message"] = $"Success/Pomyślnie potwierdzono odbiór kluczy do sali \"{reservation.roomId}\"";
            }

            return RedirectToPage("./IndexEmployees");
        }
    }
}
