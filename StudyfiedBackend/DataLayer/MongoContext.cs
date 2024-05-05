using MongoDB.Driver;
using MongoDbGenericRepository.Attributes;
using StudyfiedBackend.DataLayer.Repositories.GenericMongoRepository;
using System.Reflection;

namespace StudyfiedBackend.DataLayer
{
    public class MongoContext : IMongoContext
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;

        public MongoContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("MongoDB:ConnectionURI");
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(configuration.GetValue<string>("MongoDB:DatabaseName"));
        }

        public MongoContext(MongoClient client, IMongoDatabase database)
        {
            _client = client;
            _database = database;
        }

        public IMongoRepository<T> GetRepository<T>() where T : class
        {
            var collectionName = typeof(T).GetCustomAttributes<CollectionNameAttribute>()
              .FirstOrDefault()?.Name;
            if (collectionName == null)
            {
                throw new ArgumentException("Data model must have a CollectionName attribute");
            }
            return new MongoRepository<T>(_database.GetCollection<T>(collectionName));
        }
    }

}
