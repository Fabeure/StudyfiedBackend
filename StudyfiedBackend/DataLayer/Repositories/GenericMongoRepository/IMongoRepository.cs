using MongoDB.Driver;

namespace StudyfiedBackend.DataLayer.Repositories.GenericMongoRepository
{
    public interface IMongoRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<T> GetByIdAsync(string id);

        Task<IEnumerable<T>> GetByFilter(FilterDefinition<T> filter);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetDocumentsByIdsAsync(IEnumerable<string> ids);
        Task<bool> UpdateAsync(string id, T entity);
        Task<bool> DeleteAsync(string id);
        Task<bool> BatchDelete(IEnumerable<string> ids);
    }
}