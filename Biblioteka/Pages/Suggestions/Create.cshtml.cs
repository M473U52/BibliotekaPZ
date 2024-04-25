using Biblioteka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Biblioteka.Views.Suggestions
{
    public class CreateModel : PageModel
    {
        private readonly Biblioteka.Context.BibContext _context;
        public CreateModel(Biblioteka.Context.BibContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Suggestion suggestion { get; set; } = default!;

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {

            if (!ModelState.IsValid || suggestion == null)
            {
                return Page();
            }
            else
            {
                suggestion.votes = 1;
                _context.Suggestion.Add(suggestion);
                _context.SaveChanges();
            }

            return RedirectToPage("./Index");
        }
    }
}