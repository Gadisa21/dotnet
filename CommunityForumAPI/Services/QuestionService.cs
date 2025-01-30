using CommunityForum.DTOs;
using CommunityForum.Models;
using MongoDB.Driver;
using MongoDB.Bson;


namespace CommunityForum.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IMongoCollection<Question> _questions;

        public QuestionService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
            var database = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
            _questions = database.GetCollection<Question>(configuration["MongoDB:QuestionsCollection"]);
        }

        public async Task<Question> CreateQuestionAsync(string userId, CreateQuestionDto questionDto)
        {
            var question = new Question
            {
                Title = questionDto.Title,
                Body = questionDto.Body,
                UserId = userId, // Set the user who created the question
                CreatedAt = DateTime.UtcNow
            };

            await _questions.InsertOneAsync(question);
            return question;
        }
          public async Task<List<Question>> GetAllQuestionsAsync(int page, int pageSize)
        {
            return await _questions
                .Find(q => true) // Retrieve all questions
                .SortByDescending(q => q.CreatedAt) // Sort by newest first
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }

        public async Task<Question?> GetQuestionByIdAsync(string questionId)
        {
            return await _questions.Find(q => q.Id == questionId).FirstOrDefaultAsync();
        }

        public async Task<List<Question>> SearchQuestionsAsync(string keyword)
{
    // Case-insensitive search using MongoDB regex
    var filter = Builders<Question>.Filter.Or(
        Builders<Question>.Filter.Regex(q => q.Title, new BsonRegularExpression(keyword, "i")),
        Builders<Question>.Filter.Regex(q => q.Body, new BsonRegularExpression(keyword, "i"))
    );

    return await _questions.Find(filter).ToListAsync();  
}

    


      
    }
}