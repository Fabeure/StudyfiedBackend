namespace StudyfiedBackend.DataLayer
{
public interface IMongoRepository<T> where T : class
{
    Task<T> CreateAsync(T entity);
    Task<T> GetByIdAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<bool> UpdateAsync(string id, T entity);
    Task<bool> DeleteAsync(string id);
}
    }