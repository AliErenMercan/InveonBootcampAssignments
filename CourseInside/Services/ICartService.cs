using CourseInside.Models;

namespace CourseInside.Services
{
    public interface ICartService
    {
        Task<ServiceResult<Cart>> GetOrCreateCartAsync(string userId);
        Task<ServiceResult<Cart>> AddToCartAsync(string userId, int courseId, int quantity);
        Task<ServiceResult> RemoveFromCartAsync(int cartItemId);
        Task<ServiceResult<Cart>> GetCartAsync(string userId);
    }
}
