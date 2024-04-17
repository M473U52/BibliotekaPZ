using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;

namespace Biblioteka.Pages.Genres
{
    public class EditModel : PageModel
    {
        private IGenreRepository _genreRepository;

        public EditModel(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [BindProperty]
        public Genre Genre { get; set; } = default!;

        public IActionResult OnGet(int id)
        {          

            var genre =  _genreRepository.getOne(id);
            if (genre == null)
            {
                return NotFound();
            }
            Genre = genre;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }          
            
            try
            {
                _genreRepository.Update(Genre);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(Genre.genreId))
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

        private bool GenreExists(int id)
        {
            var isExisted = _genreRepository.getOne(id);

            return isExisted != null ? true : false;
        }
    }
}
