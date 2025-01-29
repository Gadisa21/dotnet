using CommunityForum.DTOs;
using CommunityForum.Models;
using MongoDB.Driver;

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

      
    }
}