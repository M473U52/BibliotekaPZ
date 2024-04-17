using Biblioteka.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Biblioteka.Pages.RoomReservations
{
    [Authorize(Roles = "Guest")]
    public class IndexGuestModel : PageModel
    {
        private readonly UserManager<BibUser> _userManager;
        private readonly ILogger<IndexGuestModel> _logger;

        // Dependency injection via constructor
        public IndexGuestModel(UserManager<BibUser> userManager, ILogger<IndexGuestModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            /*     do testowania
             *     var user = await _userManager.GetUserAsync(User);
                  var roles = await _userManager.GetRolesAsync(user);

                  // Check if the "Guest" role is present in the roles list
                  var isGuest = roles.Contains("Guest");
                  _logger.LogWarning("Generated email confirmation link: {isGuest}", isGuest);
            */
        }
    }
}
