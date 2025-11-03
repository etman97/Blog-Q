using Blog.Models;

namespace Blog.Services
{
    public interface IAuthService
    {
        Task<User> Authenticate(string username, string password);
    }
}
