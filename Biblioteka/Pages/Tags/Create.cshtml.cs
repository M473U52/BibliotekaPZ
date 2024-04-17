using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;

namespace Biblioteka.Pages.Tags
{
    public class CreateModel : PageModel
    {
        private ITagRepository _tagRepository;

        public CreateModel(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Tag Tag { get; set; } = default!;
        
        public IActionResult OnPost()
        {
          if (!ModelState.IsValid ||  Tag == null)
            {
                return Page();
            }
            _tagRepository.Add(Tag);
            return RedirectToPage("./Index");
        }
    }
}
