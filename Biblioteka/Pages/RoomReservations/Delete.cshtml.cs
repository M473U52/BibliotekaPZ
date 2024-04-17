using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Biblioteka.Pages.RoomReservations
{
    [Authorize(Roles = "Reader")]
    public class DeleteModel : PageModel
    {
        private IRoomReservationRepository _roomReservationRepository;

        public DeleteModel(IRoomReservationRepository roomReservationRepository)
        {
            _roomReservationRepository = roomReservationRepository;
        }

        [BindProperty]
        public RoomReservation RoomReservation { get; set; } = default!;

        public IActionResult OnGet(int id)
        {

            var roomreservation = _roomReservationRepository.getOne(id);

            if (roomreservation == null)
            {
                return NotFound();
            }
            else 
            {
                RoomReservation = roomreservation;
            }
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _roomReservationRepository.Delete(id);

            return RedirectToPage("./IndexEmployees");
        }
    }
}
