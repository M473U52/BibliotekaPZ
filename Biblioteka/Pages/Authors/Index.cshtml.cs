using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Pages.Authors
{
    //[Authorize(Roles = "Admin, Employee")]
    public class IndexModel : PageModel
    {
        private IAuthorRepository _authorRepository;

        public IndexModel(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public IList<Author> Author { get; set; } = default!;

        public void OnGet()
        {
            Author = _authorRepository.getAll();
        }
        public IActionResult OnPostDeleteBorrowing(int authorId)
        {
            var author = _authorRepository.getOne(authorId);

            if (author != null)
            {

                TempData["Message"] = $"Success/Pomyślnie usunięto autora: \"{author.name}\" \"{author.surname}\"";

                _authorRepository.Delete(authorId);
            }
            else
            {
                TempData["Message"] = $"Error/Nie ma takiego autora.";
            }
            return RedirectToPage("./Index");
        }
    }
}
