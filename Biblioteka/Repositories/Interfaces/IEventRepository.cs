using Biblioteka.Models;

namespace Biblioteka.Repositories.Interfaces
{
    public interface IEventRepository
    {
        public List<Event> getAll();
        public Event? getOne(object id);
        public void Add(Event publisher);
        public void Delete(object id);
        public void Update(Event publisher);
        public Event searchEvent(string publisherName);
    }
}
