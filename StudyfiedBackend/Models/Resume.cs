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
    
        public string? UserId { get; set; }

        public string? Topic { get; set; }
        
        public IList<string> ResumeContents { get; set; }
    
        public Resume()
        {
            this.ResumeContents = new List<string>();
            this.Topic = "";
            this.UserId = "NA";
        }   
        public bool hasUser()
        {
            return this.UserId != "NA";
        }   

    }
}
