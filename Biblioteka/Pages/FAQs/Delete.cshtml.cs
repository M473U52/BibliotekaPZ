using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Pages.FAQs
{
    public class DeleteModel : PageModel
    {
        private IFAQRepository _FAQRepository;
        public DeleteModel(Context.BibContext context, IFAQRepository FAQRepository)
        {

            _FAQRepository = FAQRepository;
        }

        [BindProperty]
        public FAQ FAQ { get; set; } = default!;

        public IActionResult OnGet(int id)
        {

            var faq = _FAQRepository.getOne(id);

            if (faq == null)
            {
                return NotFound();
            }
            else 
            {
                FAQ = faq;
            }
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _FAQRepository.Delete(id);

            return RedirectToPage("./All");
        }
    }
}
