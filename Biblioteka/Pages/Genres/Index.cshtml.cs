using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;
using Microsoft.AspNetCore.Mvc;
using Biblioteka.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Pages.Genres
{
    public class IndexModel : PageModel
    {
        private IGenreRepository _genreRepository;

        public IndexModel(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public IList<Genre> Genre { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Genre = new List<Genre> { _genreRepository.searchGenre(SearchTerm) };
            }
            else
            {
                // Jeśli nazwa nie jest określona, pobierz wszystkie wydawnictwa
                Genre = _genreRepository.getAll();
            }
        }
        public IActionResult OnPostDeleteGenre(int genreId)
        {
            var genre = _genreRepository.getOne(genreId);
            if(genre == null)
            {
                TempData["Message"] = $"Error/Brak gatunku o id: {genreId}.";
                return RedirectToPage("./Index");
            }
            _genreRepository.Delete(genreId);
            TempData["Message"] = $"Success/Pomyślnie usunięto gatunek: \"{genre.name}\" ";
            return RedirectToPage("./Index");
        }
    }
}
