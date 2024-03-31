using MongoDB.Bson;
using MongoDB.Driver;

namespace StudyfiedBackend.DataLayer
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
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            return await _collection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<bool> UpdateAsync(string id, T entity)
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            var updateResult = await _collection.ReplaceOneAsync(filter, entity);
            return updateResult.IsModifiedCountAvailable;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            var deleteResult = await _collection.DeleteOneAsync(filter);
            return deleteResult.DeletedCount > 0;
        }
    }


}
