using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Biblioteka.Pages.Borrowings
{
    [Authorize(Roles = "Employee, Admin")]
    public class IndexAdminModel : PageModel
    {
        private IBorrowingRepository _borrowingRepository;
        private IReaderRepository _readerRepository;
        private IReader_BorrowingsRepository _readerBorrowingRepository;

        public IndexAdminModel(IBorrowingRepository borrowingRepository, IReaderRepository readerRepository, IReader_BorrowingsRepository readerBorrowingRepository)
        {
            _borrowingRepository = borrowingRepository;
            _readerRepository = readerRepository;
            _readerBorrowingRepository = readerBorrowingRepository;
        }

        public IList<Borrowing> Borrowing { get; set; } = default!;

        public IList<Reader> Readers { get; set; } = default!;
        public IList<Reader_Borrowings> Reader_Borrowings { get; set; } = default!;
        public string SearchTerm { get; set; }

        public void OnGet(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                Borrowing = _borrowingRepository.SearchBorrowings(searchTerm);
            }
            else
            {
                Borrowing = _borrowingRepository.GetAll();
            }

            Readers = _readerRepository.getAll();
            Reader_Borrowings = _readerBorrowingRepository.getAll();
        }
    }
}
