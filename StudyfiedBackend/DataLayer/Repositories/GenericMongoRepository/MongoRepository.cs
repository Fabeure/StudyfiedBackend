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
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");

            var filter = Builders<T>.Filter.Eq("Id", id);

            var updates = new List<UpdateDefinition<T>>();
            var updateDefinitionBuilder = Builders<T>.Update;

            // Iterate through the properties of the entity
            foreach (var property in typeof(T).GetProperties())
            {
                var value = property.GetValue(entity);

                // Skip null or default values
                if (value != null && !IsDefaultValue(value, property.PropertyType))
                {
                    updates.Add(updateDefinitionBuilder.Set(property.Name, value));
                }
            }

            if (!updates.Any())
                throw new ArgumentException("No updates provided in the entity.", nameof(entity));

            var combinedUpdate = updateDefinitionBuilder.Combine(updates);

            var updateResult = await _collection.UpdateOneAsync(filter, combinedUpdate);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        // Helper method to check for default values
        private bool IsDefaultValue(object value, Type type)
        {
            return value.Equals(type.IsValueType ? Activator.CreateInstance(type) : null);
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
