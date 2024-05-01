using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace StudyfiedBackend.Models
{
    [CollectionName("Quiz")]
    public class Quiz
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? userId { get; set; }

        public string? topic { get; set; }

        public string? difficulty { get; set; }

        public int? numberOfQuestion { get; set; }
        public Dictionary<Question, List<Response>> questionAnswerPairs { get; set; }


        public Quiz(string topic, string difficulty, int numberOfQuestion, string userId = "NA")
        {
            this.userId = userId;
            this.topic = topic;
            this.difficulty = difficulty;
            this.questionAnswerPairs = new Dictionary<Question, List<Response>>();
            this.numberOfQuestion = numberOfQuestion;
        }

        public bool hasUser()
        {
            return this.userId != "NA";
        }
    }

}


