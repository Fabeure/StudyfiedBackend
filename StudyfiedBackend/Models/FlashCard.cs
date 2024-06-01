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
        public string Id { get; set; } = string.Empty;

        public Dictionary<string, string> items { get; set; }

        public string? userId { get; set; }

        public FlashCard(Dictionary<string, string> items, string userId = "NA")
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
