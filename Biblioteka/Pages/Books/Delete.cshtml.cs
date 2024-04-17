using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;
using Serilog;

namespace Biblioteka.Views.Books
{
    public class DeleteModel : PageModel
    {
        private IBookRepository _bookRepository;

        public DeleteModel(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            var book = _bookRepository.getOne(id);

            if (book == null)
            {
                return NotFound();
            }
            else 
            {
                Book = book;
            }
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            Log.ForContext("SaveToFile", "AnyValue").Information("Usunięto książkę " + Book.title);
            _bookRepository.Delete(id);
            return RedirectToPage("./Index");
        }
    }
}
