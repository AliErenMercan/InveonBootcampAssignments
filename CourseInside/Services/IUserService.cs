using CourseInside.Models;

namespace CourseInside.Services
{
    public interface IUserService
    {
        Task<User?> LoginAsync(string email, string password);
        Task RegisterAsync(User user, string password);
        Task<User?> GetUserByIdAsync(string id);
    }
}