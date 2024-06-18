using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

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
                TempData["Message"] = $"Error/Brak gatunku o id: {id}.";
                return RedirectToPage("./Index");
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
            if (_genreRepository.searchGenre(Genre.name) != null)
            {
                ModelState.AddModelError("", "Jest już taki gatunek");
                return Page();
            }

            try
            {
                _genreRepository.Update(Genre);
            }
            catch (DbUpdateConcurrencyException)
            {
                TempData["Message"] = $"Error/Wystąpił błąd podczas aktualizacji rekordu w bazie.";
                return RedirectToPage("./Index");
            }
            TempData["Message"] = $"Success/Pomyślnie zmodyfikowano gatunek: {Genre.name}.";
            return RedirectToPage("./Index");
        }
    }
}
