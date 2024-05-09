using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Biblioteka.Pages.Suggestions
{
    public class OrderModel : PageModel
    {
        private ISuggestionRepository _suggestionRepository;
        private IBookRepository _bookRepository;

        public OrderModel(ISuggestionRepository suggestionRepository, IBookRepository bookRepository)
        {
            _suggestionRepository = suggestionRepository;
            _bookRepository = bookRepository;
        }

        public Suggestion suggestion { get; set; } = default!;
        public Book book { get; set; }

        public void OnGet(int id)
        {
            suggestion = _suggestionRepository.getOne(id);
        }

        public IActionResult OnPost(Book book)
        {
            _bookRepository.Add(book);
            return RedirectToPage("./Index");
        }
    }
}
