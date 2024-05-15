using Biblioteka.Models;

namespace Biblioteka.Repositories.Interfaces
{
    public interface IFAQRepository
    {
        public List<FAQ> getAll();
        public FAQ? getOne(object id);
        public FAQ search(string question);
        public void Add(FAQ faq);
        public void Delete(object id);
        public void Update(FAQ faq);

    }
}
