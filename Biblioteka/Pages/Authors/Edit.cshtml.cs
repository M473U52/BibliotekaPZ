using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Biblioteka.Pages.Authors
{
    [Authorize(Roles = "Admin, Employee, Author")]
    public class EditModel : PageModel
    {
        private IAuthorRepository _authorRepository;

        public EditModel(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [BindProperty]
        public Author Author { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            var author = _authorRepository.getOne(id);

            if (id == 0)
            {
                author = _authorRepository.GetByMail(User.Identity.Name);
            }


            if (author == null)
            {
                return NotFound();
            }

            Author = author;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (User.IsInRole("Author"))
            {
                Author.email = User.Identity.Name;
                //ModelState.Values
            }

            if (Author.image != null)
                if (Author.image.Length > 0 && Author.image.Length < 10000000 && Path.GetExtension(Author.image.FileName) == ".jpg")
                {
                    using (var ms = new MemoryStream())
                    {
                        Author.image.CopyTo(ms);
                        Author.imageData = ms.ToArray();
                    }
                }
                else
                    ModelState.AddModelError("file not pdf or wrong size", "Plik musi być w formacie JPG i nie większy niż 10MB!");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _authorRepository.Update(Author);
                TempData["UpdateSuccess"] = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(Author.authorId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AuthorExists(int id)
        {
            var isExisted = _authorRepository.getOne(id);

            return isExisted != null ? true : false;
        }
    }
}
