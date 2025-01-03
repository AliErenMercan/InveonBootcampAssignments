using CourseInside.Models;
using System.Threading.Tasks;

namespace CourseInside.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> IsEmailExistAsync(string email);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task<User?> GetUserWithOrdersAsync(string userId);
    }
}
