using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBConnection.Models
{
    [BsonIgnoreExtraElements]
    public class PassedOutStudent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("email")]
        public string Email { get; set; } = null!;

        [BsonElement("address")]
        public string Address { get; set; } = null!;

        [BsonElement("admissionOn")]
        public DateTime AdmissionOn { get; set; }

        [BsonElement("course")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Course { get; set; } = new();

        [BsonElement("deletedOn")]
        public DateTime DeletedOn { get; set; } = DateTime.UtcNow;

    }
}
