using Biblioteka.Context;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Areas.Identity.Pages.Account.Manage
{
    public class NotifyFreqModel : PageModel
    {

        private readonly IReaderRepository _readerRepository;

        public NotifyFreqModel( IReaderRepository readerRepository)
        {
            _readerRepository = readerRepository;
        }

        [TempData]
        public int CurrentDaysBeforeReturn { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var reader = _readerRepository.GetByMail(userEmail);

            if (reader != null)
            {
                CurrentDaysBeforeReturn = reader.DaysBeforeReturn;
                return Page();
            }
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostAsync(int notificationDays)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var reader = _readerRepository.GetByMail(userEmail);

            if (reader != null)
            {
                reader.DaysBeforeReturn = notificationDays;

                CurrentDaysBeforeReturn = notificationDays;

                return Page();
            }

            return Page();
        }
    }
}
