using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Pages.Publishers
{
    public class DeleteModel : PageModel
    {
        private IPublisherRepository _publisherRepository;

        public DeleteModel(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        [BindProperty]
      public Publisher Publisher { get; set; } = default!;

        public IActionResult OnGet(int id)
        {

            var publisher =  _publisherRepository.getOne(id);

            if (publisher == null)
            {
                return NotFound();
            }
            else 
            {
                Publisher = publisher;
            }
            return Page();
        }

        public IActionResult OnPost(int id)
        {

            _publisherRepository.Delete(id);
            return RedirectToPage("./Index");
        }
    }
}
