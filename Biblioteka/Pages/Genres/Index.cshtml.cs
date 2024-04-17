using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;

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

        public void OnGet(string? genreName)
        {
            if (!string.IsNullOrEmpty(genreName))
            {
                Genre = new List<Genre> { _genreRepository.searchGenre(genreName) };
            }
            else
            {
                // Jeśli nazwa nie jest określona, pobierz wszystkie wydawnictwa
                Genre = _genreRepository.getAll();
            }
        }
    }
}
