using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.DbImplementations;
using Biblioteka.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Biblioteka.Repositories
{
    public class BorrowingRepository : GenericRepository<Borrowing>, IBorrowingRepository
    {
        private readonly BibContext _context;

        public BorrowingRepository(BibContext context) : base(context)
        {
            _context = context;
        }

        public void Add(Borrowing borrowing)
        {
            if (borrowing == null)
            {
                throw new ArgumentException("Object not found.", nameof(borrowing));
            }

            borrowing.bookLost = false;
            _context.Add(borrowing);
            _context.SaveChanges();
        }

        public Borrowing GetOne(int id)
        {
            return _context.Borrowing
                .Include(b => b.book)
                .ThenInclude(ba => ba.authors)
                .FirstOrDefault(m => m.borrowId == id);
        }

        public List<Borrowing> GetAll()
        {
            return _context.Borrowing
                .Include(b => b.book)
                .ThenInclude(ba => ba.authors)
                .ToList();
        }
        public void AddECP(Borrowing borrowing )
        {
            if (borrowing == null)
            {
                throw new ArgumentException("Object not found.", nameof(borrowing));
            }
            _context.EmployeeConfirmingPaymentsBook.Add(borrowing.employeeConfirmingPayment);
            _context.SaveChanges();
        }
        public void AddECR(Borrowing borrowing)
        {
            if (borrowing == null)
            {
                throw new ArgumentException("Object not found.", nameof(borrowing));
            }
            _context.EmployeeConfirmingReturnsBook.Add(borrowing.employeeConfirmingReturn);
            _context.SaveChanges();
        }

        public IList<Borrowing> SearchBorrowings(string searchTerm)
        {
            var query = _context.Borrowing
                .Include(b => b.book)
                .Include(b => b.readers)
                .ThenInclude(r => r.reader)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var searchTerms = searchTerm.Split(',').Select(term => term.Trim().ToLower()).ToArray();
                var borrowingsInMemory = query.ToList();

                var filteredBorrowings = borrowingsInMemory
                    .Where(borrowing =>
                        searchTerms.All(searchTerm =>
                            (borrowing.book.title != null && borrowing.book.title.ToLower().Contains(searchTerm)) ||
                            borrowing.readers.Any(r =>
                                (r.reader.name + " " + r.reader.surname).ToLower().Contains(searchTerm)) ||
                            (DateTime.TryParseExact(searchTerm, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date) &&
                             borrowing.borrowDate.Date == date.Date))
                    )
                    .AsQueryable();

                query = filteredBorrowings.AsQueryable();
            }

            return query.ToList();
        }

        public decimal CalculateLateFee(int borrowingId)
        {
            var borrowing = _context.Borrowing
                .Include(b => b.book)
                .FirstOrDefault(b => b.borrowId == borrowingId);

            if (borrowing != null && borrowing.returnDate.HasValue && borrowing.returnDate > borrowing.plannedReturnDate)
            {
                var daysLate = (borrowing.returnDate.Value - borrowing.plannedReturnDate).Days;
                var lateFeeRatePerDay = 0.50m; // Stała stawka za dzień spóźnienia (50 groszy)

                var lateFee = daysLate * lateFeeRatePerDay;

                // Zaokrąglij opłatę do dwóch miejsc po przecinku
                return Math.Round(lateFee, 2);
            }

            // Jeśli książka została zwrócona na czas, opłata wynosi 0
            return 0;
        }





    }
}
