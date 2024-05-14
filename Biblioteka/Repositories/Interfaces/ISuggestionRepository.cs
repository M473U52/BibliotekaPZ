using Biblioteka.Models;

namespace Biblioteka.Repositories.Interfaces
{
    public interface ISuggestionRepository
    {
        public List<Suggestion> getAll();
        public Suggestion? getOne(object id);
        public Suggestion search(string title);
        public void Add(Suggestion suggestion);
        public void Delete(object id);
        public void Update(Suggestion suggestion);
    }
}
