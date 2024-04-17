using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Repositories.DbImplementations
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        private BibContext _context;

        public EventRepository(BibContext context) : base(context)
        {
            _context = context;
        }
        public Event searchEvent(string eventName)
        {
            return _context.Event.FirstOrDefault(p => p.name.ToLower().Contains(eventName.ToLower()));
        }

        public Event GetOne(int id)
        {
            return _context.Event
                .Include(b => b.author)
                .FirstOrDefault(m => m.eventId == id);
        }

        public List<Event> GetAll()
        {
            return _context.Event
                .Include(b => b.author)
                .ToList();
        }

    }
}
