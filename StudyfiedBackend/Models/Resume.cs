using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;


namespace StudyfiedBackend.Models
{
    [CollectionName("Resume")]
    public class Resume
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
            public string? Id { get; set; }
    
            public string? userId { get; set; }

            public string? topic { get; set; }
    
            public string? resumecontent { get; set; }
    
            public Resume(string resume,string topic, string userId = "NA")
        {
                this.resumecontent = resume;
                this.topic = topic;
                this.userId = userId;
            }   
    
            public bool hasUser()
        {
                return this.userId != "NA";
            }   

    }
}
