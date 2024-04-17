using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Context;
using Biblioteka.Models;
using Microsoft.AspNetCore.Identity;
using Biblioteka.Areas.Identity.Data;
using Biblioteka.Repositories.Interfaces;
using Serilog;
namespace Biblioteka.Pages.Borrowings
{
    public class IndexReaderModel : PageModel
    {
       
        private readonly BibContext _bibContext;
        private readonly UserManager<BibUser> _userManager;
        private IBorrowingRepository _borrowingRepository;
        public IList<RoomReservation> RoomReservation { get; set; }

        public IndexReaderModel(BibContext bibContext, UserManager<BibUser> userManager, IBorrowingRepository borrowingRepository)
        {
            _userManager = userManager;
            _bibContext = bibContext;
            _borrowingRepository = borrowingRepository;
            RoomReservation = new List<RoomReservation>();
        }

        public IList<Borrowing> Borrowing { get; set; } = new List<Borrowing>();
        public IList<Reader> Readers { get; set; } = new List<Reader>();
        public IList<Reader_Borrowings> Reader_Borrowings { get; set; } = new List<Reader_Borrowings>();

        public async Task OnGetAsync()
        {
            var loggedInUserId = _userManager.GetUserId(User);

            if (loggedInUserId != null)
            {
                var user = await _userManager.FindByIdAsync(loggedInUserId);
                Log.Information("Application is starting up");
                if (user != null)
                {
                    string name = user.name;
                    string surname = user.surname;

                    var foundReader = await _bibContext.Reader.FirstOrDefaultAsync(r => r.name == name && r.surname == surname);

                    if (foundReader != null)
                    {
                        var foundBorrowings = await _bibContext.Borrowing
                            .Include(b => b.book)
                            .ThenInclude(ba => ba.authors)
                            .Where(b => b.readers.Any(rb => rb.readerId == foundReader.readerId))
                            .Include(b => b.employee)
                            .ToListAsync();

                        Borrowing = foundBorrowings;
                    }
                    else
                    {
                        Borrowing = new List<Borrowing>();
                    }
                }
            }
        }
    }

}
