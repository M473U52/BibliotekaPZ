using Biblioteka.Models;

namespace Biblioteka.Repositories.Interfaces
{
    public interface IBookOpinionRepository
    {
        public List<Book_Opinions> getOpinionsForReader(Reader reader);
        public List<Book_Opinions> getOpinionsForBook(Book book);
        public void Add(Book_Opinions opinion);
        public void DeleteOpinion(Book_Opinions opinion);
        public Book_Opinions getOpinionForReaderAndBook(Reader reader, Book book);
    }
}
