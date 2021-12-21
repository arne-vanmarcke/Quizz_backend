using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BackendApp.Models
{
    public class Highscore
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("Score")]
        public int Score { get; set; }
        public string Name { get; set; }
    }
}
