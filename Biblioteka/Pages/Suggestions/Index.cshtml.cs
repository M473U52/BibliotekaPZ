using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Pages.Suggestions
{
    public class IndexModel : PageModel
    {

        private ISuggestionRepository _suggestionRepository;
        public IndexModel(ISuggestionRepository suggestionRepository)
        {

            _suggestionRepository = suggestionRepository;
        }

        public IList<Suggestion> suggestions { get; set; } = default!;

        public void OnGet()
        {
            if (suggestions == null)
            {
                suggestions = _suggestionRepository.getAll();
            }
        }

        /*public IActionResult OnPostVote(int suggestionId)
        {
            var suggestion = _context.Suggestion.Find(suggestionId);
            if (suggestion != null)
            {
                suggestion.votes++;
                _context.SaveChanges();
                return RedirectToPage("./Index");
            }
            return Page();
        }*/

        public IActionResult OnPostVote(int suggestionId)
        {
            var suggestion = _suggestionRepository.getOne(suggestionId);
            if (suggestion != null)
            {
                var votedKey = "voted_" + suggestionId;
                if (HttpContext.Request.Cookies.ContainsKey(votedKey))
                {
                    return BadRequest("Ju¿ zag³osowa³eœ na tê sugestiê");
                }

                suggestion.votes++;
                _suggestionRepository.Update(suggestion);

                HttpContext.Response.Cookies.Append(votedKey, "true");

                return RedirectToPage("/Suggestions/Index");
            }
            return Page();
        }


    }
}