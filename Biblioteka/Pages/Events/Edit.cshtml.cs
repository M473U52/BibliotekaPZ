using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Pages.Events
{
    public class EditModel : PageModel
    {
        private IEventRepository _eventRepository;

        public EditModel( IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;

        }

        [BindProperty]
        public EventDto Event { get; set; } = new EventDto(); 

        public IActionResult OnGet(int? id)
        {
            var eventa = _eventRepository.getOne(id);
            if (eventa == null)
            {
                return NotFound();
            }
            else
            {
                Event = new EventDto
                {
                    eventId = eventa.eventId,
                    name = eventa.name,
                    description = eventa.description,
                    eventDate = eventa.eventDate
                };
            }
            return Page();
        }


        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid || Event == null)
            {
                return Page();
            }
            else
            {
                var existingEvent = _eventRepository.getOne(Event.eventId);

                if (existingEvent == null)
                {
                    return NotFound();
                }

                existingEvent.name = Event.name;
                existingEvent.description = Event.description;
                existingEvent.eventDate = Event.eventDate;

                _eventRepository.Update(existingEvent);
                return RedirectToPage("./Index");

            }
        }
    }
}
