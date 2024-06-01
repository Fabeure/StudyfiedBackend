using MongoDB.Driver;

namespace StudyfiedBackend.DataLayer.Repositories.GenericMongoRepository
{
    public class MongoRepository<T> : IMongoRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IMongoCollection<T> collection)
        {
            _collection = collection;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            return await _collection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<bool> UpdateAsync(string id, T entity)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            var updateResult = await _collection.ReplaceOneAsync(filter, entity);
            return updateResult.IsModifiedCountAvailable;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            var deleteResult = await _collection.DeleteOneAsync(filter);
            return deleteResult.DeletedCount == 1 && deleteResult.IsAcknowledged;
        }

        public async Task<bool> BatchDelete(IEnumerable<string> ids)
        {
            var filter = Builders<T>.Filter.In("Id", ids);
            var deleteResult = await _collection.DeleteManyAsync(filter);
            return deleteResult.DeletedCount == ids.Count() && deleteResult.IsAcknowledged;
        }

        public async Task<IEnumerable<T>> GetDocumentsByIdsAsync(IEnumerable<string> ids)
        {
            var objectIdList = ids.ToList();
            var filter = Builders<T>.Filter.In("Id", objectIdList);
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByFilter(FilterDefinition<T> filter)
        {
            return await _collection.FindAsync(filter).Result.ToListAsync();
        }
    }
}
