using CourseInside.Models;

namespace CourseInside.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(string id);
        Task AddUserAsync(User user);
        Task SaveChangesAsync();
    }
}