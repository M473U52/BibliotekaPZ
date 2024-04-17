using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Biblioteka.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using System.Diagnostics;

namespace Biblioteka.Pages.Borrowings
{
    [Authorize(Roles = "Admin,Reader")]
    public class CreateModel : PageModel
    {
        private readonly UserManager<BibUser> _userManager;

        private IBookRepository _bookRepository;
        private readonly ILogger<CreateModel> _logger;



        //private BibUser user;
        private Borrowing[] basket;


        public CreateModel( IBookRepository bookRepository, UserManager<BibUser> userManager, ILogger<CreateModel> logger)
        {
            _bookRepository = bookRepository;
            _userManager = userManager;
            _logger = logger;
         
            //this.user = user;;

        }
        //[BindProperty]
        public Reader_Borrowings Reader_Borrowings { get; set; } = new Reader_Borrowings();
        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;
        public Book Book { get; set; } = default!;
        [BindProperty]
        public int Id { get; set; }


        public IActionResult OnGet(int id)
        {
            Id = id;
            Debug.WriteLine("Application");
            Book = _bookRepository.getOne(id);

            
           /* Log.ForContext("SaveToFile", "AnyValue").Information("Wypo¿yczono ksi¹¿kê " + Book.title);
            Debug.WriteLine("Application");
            Debug.WriteLine("TITLE: " + Book.title);*/
            return Page();
        }
    }
}