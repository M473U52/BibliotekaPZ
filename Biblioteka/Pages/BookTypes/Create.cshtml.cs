using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;
using Biblioteka.Repositories.DbImplementations;

namespace Biblioteka.Pages.BookTypes
{
    public class CreateModel : PageModel
    {
        private IBookTypeRepository _bookTypeRepository;

        public CreateModel(IBookTypeRepository bookTypeRepository)
        {
            _bookTypeRepository = bookTypeRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BookType BookType { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
          if (!ModelState.IsValid || BookType == null)
            {
                return Page();
            }

            if (_bookTypeRepository.searchBookType(BookType.name) != null)
            {
                ModelState.AddModelError("", "Jest już dodany taki rodzaj książki");
                return Page();
            }
            else
            {
                _bookTypeRepository.Add(BookType);
            }

            return RedirectToPage("./Index");
        }
    }
}
