using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;

namespace Biblioteka.Pages.BookTypes
{
    public class IndexModel : PageModel
    {
        private IBookTypeRepository _bookTypeRepository;

        public IndexModel(IBookTypeRepository bookTypeRepository)
        {
            _bookTypeRepository = bookTypeRepository;
        }

        public IList<BookType> BookType { get;set; } = default!;

        public void OnGet(string? booktypeName)
        {
            if (!string.IsNullOrEmpty(booktypeName))
            {
                BookType = new List<BookType> { _bookTypeRepository.searchBookType(booktypeName) };
            }
            else
            {
                // Jeśli nazwa nie jest określona, pobierz wszystkie wydawnictwa
                BookType = _bookTypeRepository.getAll();
            }
        }
    }
}
