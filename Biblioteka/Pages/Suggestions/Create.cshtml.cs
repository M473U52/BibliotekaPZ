using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Biblioteka.Views.Suggestions
{
    public class CreateModel : PageModel
    {
        private ISuggestionRepository _suggestionRepository;
        public CreateModel( ISuggestionRepository suggestionRepository)
        {

            _suggestionRepository = suggestionRepository;
        }

        [BindProperty]
        public Suggestion suggestion { get; set; } = default!;

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if(_suggestionRepository.search(suggestion.title)!=null)
            {
                ModelState.AddModelError("", "Ksi¹¿ka o tym tytule jest ju¿ w sugestiach");
                return Page();
            }

            if (!ModelState.IsValid || suggestion == null)
            {
                return Page();
            }
            else
            {
                suggestion.votes = 1;
                _suggestionRepository.Add(suggestion);
            }

            return RedirectToPage("./Index");
        }
    }
}