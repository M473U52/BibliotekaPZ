using Biblioteka.Models;

namespace Biblioteka.Repositories.Interfaces
{
    public interface IPositionRepository
    {
        public Position getOne(object id);
        public void Delete(object id);
        public List<Position> getAll();
        public void Add(Position position);

    }
}
