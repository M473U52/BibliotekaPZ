using Biblioteka.Context;
using Biblioteka.Repositories;

namespace Biblioteka.Models
{
    public class DailyRaport
    {
        private BorrowingRepository _borrowingRepository;
        public int booksBorrowed { get; set; }
        public int totalBorrowed { get; set; }

        public DailyRaport(BibContext context)
        {
            _borrowingRepository = new BorrowingRepository(context);
        }

        public void getDataSince(DateTime initDate)
        {
            var allBorrowings = _borrowingRepository.GetAll();

            var searchedBorrowings = new List<Borrowing>();
            foreach (var borrow in allBorrowings)
                if (borrow.borrowDate > initDate)
                    searchedBorrowings.Add(borrow);

            decimal totalPayments = 0;
            int booksReturned = 0;
            int booksPaid = 0;
            List<Book> lostBooksList = new List<Book>();

            foreach (var borrow in searchedBorrowings)
            {
                if (borrow.LateFee != null)
                    totalPayments += borrow.LateFee;

                if (borrow.IsReturned)
                    booksReturned++;

                if (borrow.IsPaid)
                    booksPaid++;

                if (borrow.bookLost)
                    lostBooksList.Add(borrow.book);
            }

        }
    }
}
