using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Biblioteka.Areas.Identity.Data;
using Serilog;

namespace Biblioteka.Pages.Events
{
    public class CreateModel : PageModel
    {
        private IEventRepository _eventRepository;
        private IAuthorRepository _authorRepository;
        private UserManager<BibUser> _userManager;

        public CreateModel( IEventRepository eventRepository, IAuthorRepository authorRepository, UserManager<BibUser> userManager)
        {
            _eventRepository = eventRepository;
            _authorRepository = authorRepository;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public EventDto Event { get; set; } = default!;

        public async Task<IActionResult> OnPost()
        {

            if (!ModelState.IsValid || Event == null)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        var eventt = Event;
                        Console.WriteLine($"Model error: {error.ErrorMessage}");
                    }
                }
                return Page();
            }
            else
            {
                var eventt = Event;
                Event newEvent = new Event()
                {
                    name = Event.name,
                    description = Event.description,
                    eventDate = Event.eventDate,
                };

                if (Event.eventDate < DateTime.Now)
                {
                    ModelState.AddModelError("date error", "Data wydarzenia nie może być w przeszłości");
                }
                var even = _eventRepository.getAll(); 
                foreach (var item in even)
                {
                    if (item.eventDate == Event.eventDate)
                    {
                        ModelState.AddModelError("date error", "W tym momencie odbywa się już inne wydarzenie");
                    }
                }
                var loggedInUserId = _userManager.GetUserId(User);

                if (loggedInUserId != null)
                {
                    var user = await _userManager.FindByIdAsync(loggedInUserId);

                    if (user != null)
                    {
                        string email = user.Email;

                        var foundAuthor = _authorRepository.GetByMail(email);

                        if (foundAuthor != null)
                        {
                                newEvent.author = foundAuthor;
                                Log.ForContext("SaveToFile", "AnyValue").Information("Dodano wydarzenie " + Event.name);

                                _eventRepository.Add(newEvent);
                                return RedirectToPage("./Index");
                        }
                    }
                }

            }

            return RedirectToPage("./Index");
        }
    }
}
