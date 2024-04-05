using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace StudyfiedBackend.Models
{
    [CollectionName("FlashCards")]
    public class FlashCard
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string[] items { get; set; }

        public string? userId { get; set; }

        public FlashCard(string[] items, string userId = "NA")
        {
            this.items = items;
            this.userId = userId;
        }   

        public bool hasUser()
        {
            return this.userId != "NA";
        }
    }
}
