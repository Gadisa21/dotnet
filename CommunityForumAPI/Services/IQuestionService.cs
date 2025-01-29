using CommunityForum.DTOs;
using CommunityForum.Models;

namespace CommunityForum.Services
{
    public interface IQuestionService
    {
        Task<Question> CreateQuestionAsync(string userId, CreateQuestionDto questionDto);
        
    }
}