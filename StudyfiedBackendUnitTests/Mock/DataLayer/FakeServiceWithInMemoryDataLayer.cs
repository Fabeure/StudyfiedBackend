using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mongo2Go;
using MongoDB.Driver;
using StudyfiedBackend.DataLayer;
using System.Diagnostics;

namespace StudyfiedBackendUnitTests.Mock.DataLayer
{
    public class FakeServiceWithInMemoryDataLayer : IDisposable
    {

        public static int totalTests = 4;
        private readonly WebApplicationFactory<Program> _appFactory;
        public MongoDbRunner Runner { get; }
        public MongoClient Client { get; }
        public IMongoDatabase Database { get; }

        public HttpClient FakeHttpClient { get; }

        public FakeServiceWithInMemoryDataLayer()
        {
            Runner = MongoDbRunner.Start();
            Client = new MongoClient(Runner.ConnectionString);
            Database = Client.GetDatabase("InMemoryDb");

            // create fake mongo context:
            IMongoContext fakeContext = new MongoContext(client: Client, database: Database);

            // Create WebApplicationFactory for in-memory testing
            _appFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        // Replace existing MongoDB context
                        services.RemoveAll<IMongoContext>();
                        services.AddSingleton<IMongoContext>((_) => fakeContext);
                    });
                });
            FakeHttpClient = _appFactory.CreateClient();
        }

        public void Dispose()
        {
            totalTests--;
            if (totalTests > 0)
            {
                Client.DropDatabase("InMemoryDb");
            }
            if (totalTests == 0)
            {
                Runner.Dispose();
            }
        }
    }

    [CollectionDefinition("Database collection")]
    public class DatabaseCollectionWithWebAppFactory : ICollectionFixture<FakeServiceWithInMemoryDataLayer>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
