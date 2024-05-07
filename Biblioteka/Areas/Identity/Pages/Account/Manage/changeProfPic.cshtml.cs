using Biblioteka.Areas.Identity.Data;
using Biblioteka.Context;
using Biblioteka.Repositories;
using Biblioteka.Repositories.DbImplementations;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Abstractions;
using System.Security.Claims;

namespace Biblioteka.Areas.Identity.Pages.Account.Manage
{
    public class changeProfPicModel : PageModel
    {
        private readonly SignInManager<BibUser> _signInManager;
        private readonly UserManager<BibUser> _userManager;

        public changeProfPicModel(
            UserManager<BibUser> userManager,
            SignInManager<BibUser> signInManager,
            BibContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IFormFile image { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            BibUser userToUpdate = _userManager.FindByEmailAsync(User.Identity.Name).Result;

            if(userToUpdate != null)
            if(image != null)
                if (image.Length > 0 && image.Length < 10000000 && Path.GetExtension(image.FileName) == ".jpg")
                {
                    using (var ms = new MemoryStream())
                    {
                        image.CopyTo(ms);
                        userToUpdate.profilePicData = ms.ToArray();
                    }
                }
                else
                    ModelState.AddModelError("file not pdf or wrong size", "Plik musi byæ w formacie JPG i nie wiêkszy ni¿ 10MB!");

            if(ModelState.IsValid)
            {
                _userManager.UpdateAsync(userToUpdate);
            }
        }
    }
}
