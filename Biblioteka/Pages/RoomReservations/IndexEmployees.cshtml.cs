using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Biblioteka.Pages.RoomReservations
{
    [Authorize(Roles = "Admin, Employee, Reader")]
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
    }
}
