using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Serilog;

namespace Biblioteka.Pages.Events
{
    public class DeleteModel : PageModel
    {

        private IEventRepository _eventRepository;

        public DeleteModel(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [BindProperty]
      public Event Event { get; set; } = default!;

        public IActionResult OnGet(int id)
        {

            var eventa = _eventRepository.getOne(id);

            if (eventa == null)
            {
                return NotFound();
            }
            else
            {
                Event = eventa;
            }
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            Log.ForContext("SaveToFile", "AnyValue").Information("Usunięto wydarzenie " + Event.name);
            _eventRepository.Delete(id);
            return RedirectToPage("./Index");
        }
    }
}
