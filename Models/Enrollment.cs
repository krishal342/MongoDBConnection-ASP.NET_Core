using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBConnection.Models
{
    public class Enrollment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("student")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Student { get; set; } = null!;

        [BsonElement("course")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Course { get; set; } = null!;

        [BsonElement("enrolledOn")]
        public DateTime EnrolledOn { get; set; } = DateTime.UtcNow;
    }
}
