using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Pages.BookTypes
{
    public class EditModel : PageModel
    {
        private IBookTypeRepository _bookTypeRepository;

        public EditModel(IBookTypeRepository bookTypeRepository)
        {
            _bookTypeRepository = bookTypeRepository;
        }

        [BindProperty]
        public BookType BookType { get; set; } = default!;

        public IActionResult OnGet(int id)
        {

            var booktype =  _bookTypeRepository.getOne(id);
            if (booktype == null)
            {
                return NotFound();
            }
            BookType = booktype;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _bookTypeRepository.Update(BookType);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookTypeExists(BookType.typeId))
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

        private bool BookTypeExists(int id)
        {
            var isExisted = _bookTypeRepository.getOne(id);

            return isExisted != null ? true : false;
        }
    }
}
