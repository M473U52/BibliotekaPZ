using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Biblioteka.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace Biblioteka.Pages.RoomReservations
{
    [Authorize(Roles = "Reader")]
    public class IndexReaderModel : PageModel
    {
        private readonly UserManager<BibUser> _userManager;
        private IRoomReservationRepository _roomReservationRepository;

        //
        public IList<RoomReservation> RoomReservation { get; set; } = default!;

        public IndexReaderModel( UserManager<BibUser> userManager, IRoomReservationRepository roomReservationRepository)
        {
            _userManager = userManager;
            _roomReservationRepository = roomReservationRepository;

            RoomReservation = new List<RoomReservation>();
        }

        

        public async Task OnGet()
        {

            if (User.IsInRole("Reader"))
            {
                RoomReservation = _roomReservationRepository.getAll()
                .Where(rr => rr.reader.email == User.Identity.Name)
                .ToList();
            }
        }
    }
}
