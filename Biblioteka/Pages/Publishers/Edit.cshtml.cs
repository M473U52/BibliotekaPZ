using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Pages.Publishers
{
    public class EditModel : PageModel
    {
        private IPublisherRepository _publisherRepository;

        public EditModel(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        [BindProperty]
        public Publisher Publisher { get; set; } = default!;

        public async Task<IActionResult> OnGet(int id)
        {

            var publisher =  _publisherRepository.getOne(id);
            if (publisher == null)
            {
                return NotFound();
            }
            Publisher = publisher;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
           
            try
            {
                _publisherRepository.Update(Publisher);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherExists(Publisher.publisherId))
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

        private bool PublisherExists(int id)
        {
            var isExisted = _publisherRepository.getOne(id);

            return isExisted != null ? true : false;
        }
    }
}
