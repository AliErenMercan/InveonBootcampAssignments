using CourseInside.Models;

namespace CourseInside.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(string id);
        Task AddAsync(User user);
        Task SaveChangesAsync();
    }
}
