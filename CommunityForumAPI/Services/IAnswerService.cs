using CommunityForum.DTOs;
using CommunityForum.Models;

namespace CommunityForum.Services
{
    public interface IAnswerService
    {
        Task<Answer> CreateAnswerAsync(string userId, string questionId, CreateAnswerDto answerDto);
        Task<List<Answer>> GetAnswersByQuestionIdAsync(string questionId);
    }
}