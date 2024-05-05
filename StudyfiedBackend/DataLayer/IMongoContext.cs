using StudyfiedBackend.DataLayer.Repositories.GenericMongoRepository;

namespace StudyfiedBackend.DataLayer
{
    public interface IMongoContext
    {
        IMongoRepository<T> GetRepository<T>() where T : class;
    }
}
