using Biblioteka.Models;

namespace Biblioteka.Repositories.Interfaces
{
    public interface IRoomTypeRepository
    {
        public List<RoomType> getAll();
        public RoomType? getOne(object id);
    }
}