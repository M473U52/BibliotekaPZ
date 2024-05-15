using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;
using Biblioteka.Repositories.DbImplementations;

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
            if (_tagRepository.searchTag(Tag.name) != null)
            {
                ModelState.AddModelError("", "Jest już dodany taki tag");
                return Page();
            }
            else
            {
                _tagRepository.Add(Tag);
            }
            return RedirectToPage("./Index");
        }
    }
}
