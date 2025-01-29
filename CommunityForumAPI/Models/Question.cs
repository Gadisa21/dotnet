using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace CommunityForum.Models
{
    public class Question
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }  // MongoDB auto-generates this
      
        public required string Title { get; set; } = string.Empty;
        
        public required string Body { get; set; } = string.Empty;
       
        public required string UserId { get; set; } // The user who asked the question

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}