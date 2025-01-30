using CommunityForum.DTOs;
using CommunityForum.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CommunityForum.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IMongoCollection<Answer> _answers;

        public AnswerService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
            var database = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
            _answers = database.GetCollection<Answer>("Answers");
        }

        // Create an answer for a specific question
        public async Task<Answer> CreateAnswerAsync(string userId, string questionId, CreateAnswerDto answerDto)
        {
            var answer = new Answer
            {
                Body = answerDto.Body,
                UserId = userId,
                QuestionId = questionId,
                CreatedAt = DateTime.UtcNow
            };

            await _answers.InsertOneAsync(answer);
            return answer;
        }

        // Get all answers for a specific question
        public async Task<List<Answer>> GetAnswersByQuestionIdAsync(string questionId)
        {
            return await _answers.Find(a => a.QuestionId == questionId).ToListAsync();
        }
    }
}