using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;
using Microsoft.AspNetCore.Identity;
using Biblioteka.Areas.Identity.Data;
using Biblioteka.Repositories.DbImplementations;

namespace Biblioteka.Views.Books
{
    public class AuthorBooksModel : PageModel
    {
        private IBookRepository _bookRepository;
        private IAuthorRepository _authorRepository;
        private readonly UserManager<BibUser> _userManager;
        public AuthorBooksModel( IBookRepository bookRepository, UserManager<BibUser> userManager, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _userManager = userManager;
            _authorRepository = authorRepository;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public IEnumerable<Book> Book { get; set; }
        public int AuthorIdForLoggedUser { get; set; }

        public async Task OnGetAsync()
        {
            var loggedInUserId = _userManager.GetUserId(User);

            if (loggedInUserId != null)
            {
                var user = await _userManager.FindByIdAsync(loggedInUserId);

                if (user != null)
                {
                    string email = user.Email;

                    var foundAuthor = _authorRepository.GetByMail(email);

                    if (foundAuthor != null)
                    {

                        Book = _bookRepository.getAll()
                            .Where(b => b.authors.Any(a => a.authorId == foundAuthor.authorId))
                            .ToList();
                    }
                     
                }

            }

        }
      
        /* public void OnPost(int bookId)
         {
             Book book = _bookRepository.getOne(bookId);
             if (book.availableCopys < 1)
             {
                 TempData["ErrorMessage"] = "No copies available for borrowing.";
                 Page();
             }

             RedirectToPage("../Books/Create");
         }*/
    }
}