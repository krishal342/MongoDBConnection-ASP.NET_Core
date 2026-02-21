using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBConnection.Models
{
    [BsonIgnoreExtraElements]
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

        [BsonElement("currentStudent")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> CurrentStudent { get; set; } = new();

        [BsonElement("passedOutStudent")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> PassedOutStudent { get; set; } = new();



    }
}
