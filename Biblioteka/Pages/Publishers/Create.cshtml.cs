using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Pages.Publishers
{
    public class CreateModel : PageModel
    {
        private IPublisherRepository _publisherRepository;

        public CreateModel(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Publisher Publisher { get; set; } = default!;
        
        public IActionResult OnPost()
        {
          if (!ModelState.IsValid ||Publisher == null)
            {
                return Page();
            }
            _publisherRepository.Add(Publisher);
            return RedirectToPage("./Index");
        }
    }
}
