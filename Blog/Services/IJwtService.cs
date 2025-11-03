using Blog.Models;

namespace Blog.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
