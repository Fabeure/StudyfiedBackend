namespace StudyfiedBackend.DataLayer
{
    public interface IMongoContext
    {
        IMongoRepository<T> GetRepository<T>() where T : class;
    }
}
