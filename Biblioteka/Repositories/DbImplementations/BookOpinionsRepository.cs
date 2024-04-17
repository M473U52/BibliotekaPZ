using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Repositories.DbImplementations
{
    public class BookOpinionsRepository : GenericRepository<Book_Opinions>, IBookOpinionRepository
    {
        private readonly BibContext _context;

        public BookOpinionsRepository(BibContext context):base(context)
        {
            _context = context;
        }

        public List<Book_Opinions> getOpinionsForBook(Book book)
        {
           return _context.Book_Opinions.Include(r=>r.reader).ToList().Where(bo=>bo.bookId == book.bookId).ToList();
        }

        public List<Book_Opinions> getOpinionsForReader(Reader reader)
        {
            return _context.Book_Opinions.Include(b => b.book).ToList().Where(bo => bo.readerId == reader.readerId).ToList();
        }
        public Book_Opinions getOpinionForReaderAndBook(Reader reader, Book book)
        {
            return _context.Book_Opinions
                .Include(bo => bo.book)
                .Include(bo => bo.reader)
                .Where(bo => (bo.readerId == reader.readerId && bo.bookId == book.bookId)).FirstOrDefault();
        }
        public void DeleteOpinion(Book_Opinions opinion)
        {
            _context.Book_Opinions.Remove(opinion);
            _context.SaveChanges();
        }
    }
}
