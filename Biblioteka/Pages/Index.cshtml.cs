using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Context;
using Biblioteka.Models;
using Microsoft.AspNetCore.Identity;
using Biblioteka.Areas.Identity.Data;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Services;

namespace Biblioteka.Pages
{
    public class IndexModel : PageModel
    {

        private readonly BibContext _bibContext;
        private readonly UserManager<BibUser> _userManager;
        private IBorrowingRepository _borrowingRepository;
        private readonly IEmailSender _emailSender;
        private readonly IEventRepository _eventRepository;
        private readonly IBookRepository _bookRepo;

        public IndexModel(BibContext bibContext, IBookRepository bookRepo, IEventRepository eventRepository, UserManager<BibUser> userManager, IBorrowingRepository borrowingRepository, IEmailSender emailSender)
        {
            _userManager = userManager;
            _bibContext = bibContext;
            _borrowingRepository = borrowingRepository;
            _emailSender = emailSender;
            _eventRepository = eventRepository;
            _bookRepo = bookRepo;
        }
        public IList<Borrowing> Borrowing { get; set; } = default!;
        public IList<Reader> Readers { get; set; } = default!;
        public IList<Reader_Borrowings> Reader_Borrowings { get; set; } = default!;

        public IList<Event> Events { get; set; } = default!;

        public IList<Book> NewPublications { get; set; } = default!;
        public IList<string> UpcomingEvent { get; set; } = new List<string>();

        private async Task SendReminderEmailAsync(string userEmail, string subject, string message)
        {
            await _emailSender.SendEmailAsync(userEmail, subject, message);
        }

        public async Task OnGet()
        {
            var loggedInUserId = _userManager.GetUserId(User);

            if (loggedInUserId != null)
            {
                var user = await _userManager.FindByIdAsync(loggedInUserId);

                if (user != null)
                {
                    string name = user.name;
                    string surname = user.surname;

                    var foundReader = _bibContext.Reader.FirstOrDefault(r => r.name == name && r.surname == surname);

                    if (foundReader != null)
                    {
                        var foundBorrowings = await _bibContext.Borrowing
                            .Include(b => b.book)
                            .ThenInclude(ba => ba.authors)
                            .Where(b =>
                                b.readers.Any(rb => rb.readerId == foundReader.readerId) &&
                                b.IsReturned != true)
                            .ToListAsync();

                        if (foundBorrowings != null)
                        {
                            Borrowing = foundBorrowings;

                            foreach (var item in Borrowing)
                            {
                                var today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                                var startDate = today;
                                var endDate = item.plannedReturnDate;
                                var timeDifference = endDate - startDate;
                                var daysDifference = (int)Math.Floor(timeDifference.TotalDays);

                                var notificationDays = foundReader.DaysBeforeReturn;
                                ViewData["DaysBeforeReturn"] = notificationDays;
                                if (daysDifference <= notificationDays && daysDifference >= 0)
                                {
                                    var subject = "Przypomnienie o zwrocie ksi¹¿ki";
                                    var daysRemaining = daysDifference == 0 ? "dziœ" : $"{daysDifference} {(daysDifference == 1 ? "dzieñ" : "dni")}";
                                    var message = $"Zbli¿a siê termin zwrotu ksi¹¿ki {item.book.title}. Prosimy o zwrócenie jej w terminie. Masz jeszcze {daysRemaining} na zwrot.";

                                    // Wysy³anie powiadomienia e-mail
                                    await SendReminderEmailAsync(user.Email, subject, message);
                                }
                     
                            }
                        }
                        else
                        {
                            Borrowing = null;
                        }

                        var readerEvents = await _bibContext.Reader_Events
                        .Include(re => re.eventt)
                        .Where(re => re.readerId == foundReader.readerId)
                        .ToListAsync();

                        foreach (var readerEvent in readerEvents)
                        {
                            var eventDate = readerEvent.eventt.eventDate;
                            var daysUntilEvent = (eventDate - DateTime.Now).TotalDays;

                            if (daysUntilEvent <= 2 && daysUntilEvent >= 0)
                            {
                                UpcomingEvent.Add($"Zbli¿a siê wydarzenie: {readerEvent.eventt.name} w dniu {eventDate.ToShortDateString()}.");
                            }
                        }
                    }
                    else
                    {
                        Borrowing = null;
                    }
                }
            }

            var now = DateTime.Now;
            var lastMonth = now.AddDays(-30);
            var events = _eventRepository.getAll();
            
            Events = events.OrderBy(d => d.eventDate).Where(d => d.eventDate >= now).ToList();

            var books = _bookRepo.getAll();

            NewPublications = books.OrderByDescending(d => d.releaseDate).Where(d => d.releaseDate >= lastMonth && d.releaseDate <= now).ToList();
        }



        public void OnPostGetAll()
        {
        }
    }
}