using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Pages.Authors
{
    //[Authorize(Roles = "Admin, Employee")]
    public class CreateModel : PageModel
    {
        private IAuthorRepository _authorRepository;

        public CreateModel(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Author Author { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
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
            
            if (!ModelState.IsValid || Author == null)
            {
                return Page();
            }

            _authorRepository.Add(Author);

            return RedirectToPage("./Index");
        }
    }
}
