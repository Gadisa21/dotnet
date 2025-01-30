using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CommunityForum.Models
{
    public class Answer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }  // MongoDB auto-generates this

        public string Body { get; set; } = string.Empty;
        public string UserId { get; set; } // The user who answered the question
        public string QuestionId { get; set; } // The question that this answer relates to

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}