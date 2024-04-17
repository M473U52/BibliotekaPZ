using Biblioteka.Models;

namespace Biblioteka.Repositories.Interfaces
{
    public interface IBorrowingRepository
    {
        public void Add(Borrowing borrowing);
        public void Delete(object id);
        public Borrowing GetOne(int id);
        public void AddECR(Borrowing borrowing);
        public void AddECP(Borrowing borrowing);
        public List<Borrowing> GetAll();
        public void Update(Borrowing borrowing);
        public IList<Borrowing> SearchBorrowings(string searchTerm);
        public decimal CalculateLateFee(int borrowId);
    }
}
