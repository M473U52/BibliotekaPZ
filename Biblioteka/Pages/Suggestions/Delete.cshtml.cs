using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Bson;

namespace Biblioteka.Pages.Suggestions
{
    public class DeleteModel : PageModel
    {
        private ISuggestionRepository _suggestionRepository;
        
        public DeleteModel(ISuggestionRepository suggestionRepository)
        {
            _suggestionRepository = suggestionRepository;
        }

        public Suggestion suggestion { get; set; } = default!;

        public void OnGet(int id)
        {
            suggestion = _suggestionRepository.getOne(id);
        }

        public IActionResult OnPost(int id)
        {
            _suggestionRepository.Delete(id);
            return RedirectToPage("./Index");
        }
    }
}
