using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Biblioteka.Pages.FAQs
{
    public class IndexModel : PageModel
    {
        private IFAQRepository _FAQRepository;
        public IndexModel(Context.BibContext context, IFAQRepository FAQRepository)
        {

            _FAQRepository = FAQRepository;
        }

        public IList<FAQ> FAQList { get; set; } = default!;

        public void OnGet()
        {
   
            if (FAQList == null)
            {
                FAQList = _FAQRepository.getAll();
            }
        }
    }
}
