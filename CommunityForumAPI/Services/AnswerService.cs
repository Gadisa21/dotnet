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
       public async Task<bool> UpdateAnswerAsync(string answerId, string userId, string updatedBody)
    {
        var filter = Builders<Answer>.Filter.Eq(a => a.Id, answerId) &
                     Builders<Answer>.Filter.Eq(a => a.UserId, userId);
        var update = Builders<Answer>.Update.Set(a => a.Body, updatedBody);

        var result = await _answers.UpdateOneAsync(filter, update);
        return result.ModifiedCount > 0;
    }
        public async Task<bool> DeleteAnswerAsync(string answerId, string userId)
    {
        var filter = Builders<Answer>.Filter.Eq(a => a.Id, answerId) &
                     Builders<Answer>.Filter.Eq(a => a.UserId, userId);

        var result = await _answers.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }
    }
}