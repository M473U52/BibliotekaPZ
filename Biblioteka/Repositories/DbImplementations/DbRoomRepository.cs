using Microsoft.EntityFrameworkCore;
using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Repositories.DbImplementations
{
    public class DbRoomRepository : GenericRepository<Room>, IRoomRepository
    {
        private readonly BibContext _context;

        public DbRoomRepository(BibContext context) : base(context)
        {
            _context = context;
        }
        public Room GetOne(int roomNumber)
        {
            foreach (var room in getAll())
            {
                if (room.roomNumber == roomNumber)
                    return room;
            }
            return null;
        }

        public List<Room> getAll()
        {
            return _context.Room
              .Include(b => b.roomType)
              .ToList();
        }

    }
}
