using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Pages.FAQs
{
    public class CreateModel : PageModel
    {
        private IFAQRepository _FAQRepository;
        public CreateModel(Context.BibContext context, IFAQRepository FAQRepository)
        {

            _FAQRepository = FAQRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public FAQ FAQ { get; set; } = default!;
        
        public IActionResult OnPost()
        {            
            if (!ModelState.IsValid ||  FAQ == null)
            {
                return Page();
            }

            if (_FAQRepository.getAll().Count() <= 0)
            {
                //FAQ.FAQId = 1;

                _FAQRepository.Add(FAQ);
                return RedirectToPage("./All");
            }
            else
            {
                var temp = _FAQRepository.getAll().OrderByDescending(f => f.FAQId).FirstOrDefault();

                if (temp != null)
                {
                    //FAQ.FAQId = temp.FAQId + 1;

                    _FAQRepository.Add(FAQ);
                    return RedirectToPage("./All");
                }
                else
                    return NotFound();
            }
        }
    }
}
