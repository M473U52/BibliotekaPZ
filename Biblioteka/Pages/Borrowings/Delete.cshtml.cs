using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Serilog;
using Biblioteka.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace Biblioteka.Pages.Borrowings
{
    //[Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private IBorrowingRepository _borrowingRepository;
        private IBookRepository _bookRepository;
        private readonly UserManager<BibUser> _userManager;

        public DeleteModel(IBorrowingRepository borrowingRepository, IBookRepository bookRepository, UserManager<BibUser> userManager)
        {
            _borrowingRepository = borrowingRepository;
            _bookRepository = bookRepository;
            _userManager = userManager;
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;

        [BindProperty]
        public int Id {  get; set; }

        [BindProperty]
        public Book Book { get; set; }

        public IActionResult OnGet(int id)
        {

            var borrowing = _borrowingRepository.GetOne(id);
            Book = borrowing.book;
           

            if (borrowing == null)
            {
                return NotFound();
            }
            else 
            {
                Borrowing = borrowing;
            }

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var borrowing = _borrowingRepository.GetOne(id);
            Book book = borrowing.book;

            //Book book = _bookRepository.getOne(Id);
            Book = _bookRepository.getOne(id);
            Log.ForContext("SaveToFile", "AnyValue").Information("zwrócono książkę " + book.title);

            _borrowingRepository.Delete(id);
            // po usunieciu wypozyczenia przywroc liczbe dostepnych ksiazek o 1
            book.availableCopys += 1;
            _bookRepository.Update(book);
            if (User.IsInRole("Reader") && !User.IsInRole("Employee")){
                return RedirectToPage("./IndexReader");
            }
            else
            {
                return RedirectToPage("./IndexAdmin");
            }        
        }
    }
}
