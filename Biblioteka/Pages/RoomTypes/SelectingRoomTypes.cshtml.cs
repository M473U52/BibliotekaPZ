using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Biblioteka.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Biblioteka.Context;

namespace Biblioteka.Pages.RoomTypes
{
    [Authorize(Roles = "Reader")]
    public class SelectingRoomTypes : PageModel
    {
        private readonly UserManager<BibUser> _userManager;
        private IRoomTypeRepository _roomTypes;

        public IList<RoomType> RoomTypes { get; set; }

        public SelectingRoomTypes(UserManager<BibUser> userManager, IRoomTypeRepository roomTypes)
        {
            _userManager = userManager;
            _roomTypes = roomTypes;
        }



        public async Task OnGet()
        {

            RoomTypes = _roomTypes.getAll();
        }
    }
}
