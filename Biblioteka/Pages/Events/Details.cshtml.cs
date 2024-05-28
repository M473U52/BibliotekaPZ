using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using System.Security.Claims;
using Biblioteka.Context;
using Biblioteka.Repositories.DbImplementations;

namespace Biblioteka.Pages.Events
{
    public class DetailsModel : PageModel
    {
        private IEventRepository _eventRepository;
        private IReaderEventsRepository _readerEventRepository;
        // private GenericRepository<Reader_Events> _readerEventRepository;
        private IReaderRepository _readerRepository;
        private BibContext _context;

        public DetailsModel(BibContext context, IEventRepository eventRepository, IReaderRepository readerRepository,
            IReaderEventsRepository reader_Events)
        {
            _context = context;
            _eventRepository = eventRepository;
            _readerRepository = readerRepository;
            _readerEventRepository = reader_Events;
        }

        public Event Event { get; set; } = default!;
        // public Reader_Events ReaderEvents { get; set; }

        public IActionResult OnGet(int? id)
        {
            var eventa = _eventRepository.getOne(id);
            if (eventa == null)
            {
                return NotFound();
            }else {
                Event = eventa;
            }
            return Page();
        }

        public IActionResult OnPost(int eventId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var reader = _readerRepository.getOneByEmail(userEmail);

            if (reader == null)
            {
                return NotFound();
            }

            var readerEvent = new Reader_Events
            {
                eventId = eventId,
                readerId = reader.readerId
            };

            _readerEventRepository.Add(readerEvent);

            TempData["SuccessMessage"] = "Pomyślnie zapisałeś się do wydarzenia!";
            return RedirectToPage("/Index");
        }

    }
}
