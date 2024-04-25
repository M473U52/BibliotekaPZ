using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;

namespace Biblioteka.Pages.Suggestions
{
    public class IndexModel : PageModel
    {

        private readonly Context.BibContext _context;
        public IndexModel(Context.BibContext context)
        {
            _context = context;
        }

        public IList<Suggestion> suggestions { get; set; } = default!;

        public void OnGet()
        {
            if (suggestions == null)
            {
                suggestions = _context.Suggestion.ToList();
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
            var suggestion = _context.Suggestion.Find(suggestionId);
            if (suggestion != null)
            {
                var votedKey = "voted_" + suggestionId;
                if (HttpContext.Request.Cookies.ContainsKey(votedKey))
                {
                    return BadRequest("Ju¿ zag³osowa³eœ na tê sugestiê");
                }

                suggestion.votes++;
                _context.SaveChanges();

                HttpContext.Response.Cookies.Append(votedKey, "true");

                return RedirectToPage("/Suggestions/Index");
            }
            return Page();
        }


    }
}