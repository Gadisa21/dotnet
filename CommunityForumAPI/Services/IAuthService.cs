using CommunityForum.Models;

namespace CommunityForum.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(User user);
        Task<string?> LoginAsync(string email, string password);
    }
}
