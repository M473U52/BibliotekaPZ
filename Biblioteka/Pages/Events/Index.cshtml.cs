using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Microsoft.AspNetCore.Identity;
using Biblioteka.Areas.Identity.Data;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Pages.Events
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<BibUser> _userManager;
        private IEventRepository _eventRepository;
        private IAuthorRepository _authorRepository;
        public IndexModel( IEventRepository eventRepository, IAuthorRepository authorRepository, UserManager<BibUser> userManager)
        {
            _userManager = userManager;
            _authorRepository = authorRepository;
            _eventRepository = eventRepository;
        }

        public IList<Event> Event { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_eventRepository.getAll() != null)
            {
                if (User.IsInRole("Admin") || User.IsInRole("Employee"))
                {
                    Event = _eventRepository.getAll();
                }
                else if(User.IsInRole("Author"))
                {
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
                                Event = _eventRepository.getAll().Where(e => e.author.authorId == foundAuthor.authorId).ToList();
                            }
                        }
                    }
                }
            }
        }
    }
}
