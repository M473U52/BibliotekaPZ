using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace Biblioteka.Pages.Authors
{
    //[Authorize(Roles = "Admin, Employee")]
    public class AddAuthorModel : PageModel
    {
        private readonly UserManager<BibUser> _userManager;
        private IAuthorRepository _authorRepository;
        private IReaderRepository _readerRepository;

        public AddAuthorModel(
            IAuthorRepository authorRepository, 
            IReaderRepository readerRepository, 
            UserManager<BibUser> userManager)
        {
            _authorRepository = authorRepository;
            _readerRepository = readerRepository;
            _userManager = userManager;
        }
        public List<SelectListItem>? Reader { get; set; }
        public IActionResult OnGet()
        {
            Reader = _readerRepository.getAll().Select(r => new SelectListItem { Value = r.readerId.ToString(), Text = r.name + " " + r.surname }).ToList();
            return Page();
        }

        [BindProperty]
        public string ReaderId { get; set; }
        public Author Author { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid || ReaderId == null)
            {
                return Page();
            }
            Reader? foundReader = _readerRepository.getAll().FirstOrDefault(r => r.readerId.ToString().Equals(ReaderId.ToString()));
            if (foundReader == null)
            {
                ModelState.AddModelError("readerNotFound", "Nie istnieje konto czytelnika z podanym emailem");
                return Page();
            }
            else 
            {
                Author = new Author();
                BibUser user =  await _userManager.FindByEmailAsync(foundReader.email);
                await _userManager.AddToRoleAsync(user, "Author");
                foundReader.isAuthor = true;

                 Author.name = foundReader.name;
                 Author.surname = foundReader.surname;
                 Author.birthDate = foundReader.birthDate ?? default(DateTime);
                 Author.email = foundReader.email;
                _authorRepository.Add(Author);
            }
            return RedirectToPage("./Index");
        }
    }
}
