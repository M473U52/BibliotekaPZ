using Biblioteka.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Biblioteka.Areas.Identity.Pages.Account.Manage
{
    public class ConfirmPasswordResetModel : PageModel
    {
        private readonly UserManager<BibUser> _userManager;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public ResetPasswordInputModel Input { get; set; }

        public class ResetPasswordInputModel
        {
            public string UserId { get; set; }

            public string Code { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string NewPassword { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
            public string ConfirmPassword { get; set; }
        }


        public ConfirmPasswordResetModel(UserManager<BibUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }
            var user = await _userManager.FindByIdAsync(userId);

            Input = new ResetPasswordInputModel
            {
                UserId = userId,
                Code = code
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            /*if (!ModelState.IsValid)
            {
                return Page();
            }*/

            var user = await _userManager.FindByIdAsync(Input.UserId);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{Input.UserId}'.");
            }

            var decodedCode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(Input.Code));
            var result = await _userManager.ResetPasswordAsync(user, decodedCode, Input.NewPassword);

            if (result.Succeeded)
            {
                StatusMessage = "Your password has been changed";
                return Page();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
        }
    }
}
