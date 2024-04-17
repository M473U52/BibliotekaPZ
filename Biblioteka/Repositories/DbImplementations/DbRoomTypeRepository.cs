using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Repositories.DbImplementations;

namespace Biblioteka.Repositories.DbImplementations
{
    public class DbRoomTypeRepository : GenericRepository<RoomType>, IRoomTypeRepository
    {
        private BibContext _context;

        public DbRoomTypeRepository(BibContext context) : base(context)
        {
            _context = context;
        }
    }
}
