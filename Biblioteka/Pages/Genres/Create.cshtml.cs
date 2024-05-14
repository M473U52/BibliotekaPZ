using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Biblioteka.Pages.Genres
{
    public class CreateModel : PageModel
    {
        private IGenreRepository _genreRepository;

        public CreateModel(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Genre Genre { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
          if (!ModelState.IsValid ||  Genre == null)
            {
                return Page();
            }
            if (_genreRepository.searchGenre(Genre.name) != null)
            {
                ModelState.AddModelError("", "Jest już taki gatunek");
                return Page();
            }
            else
            {
                _genreRepository.Add(Genre);
            }

            return RedirectToPage("./Index");
        }
    }
}
