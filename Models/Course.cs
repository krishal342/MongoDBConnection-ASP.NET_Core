using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBConnection.Models
{
    public class Course
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; } = null!;

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("creditHours")]
        public int CreditHours { get; set; }

        [BsonElement("student")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Student { get; set; } = new();
    }
}
