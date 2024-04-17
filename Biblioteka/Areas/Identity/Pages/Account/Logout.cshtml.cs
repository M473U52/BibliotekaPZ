// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Biblioteka.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using Biblioteka.Context;

namespace Biblioteka.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<BibUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly UserManager<BibUser> _userManager;
        
        public LogoutModel(BibContext bibContext,SignInManager<BibUser> signInManager, ILogger<LogoutModel> logger, UserManager<BibUser> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            var loggedInUserId = _userManager.GetUserId(User);

            var user = await _userManager.FindByIdAsync(loggedInUserId);
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();
            Log.ForContext("SaveToFile", "AnyValue").Information("Wylogowanie " + user.Email + " - " + role);
            await _signInManager.SignOutAsync();
            
            _logger.LogInformation("User logged out.");
            
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToPage();
            }
        }
    }
}
