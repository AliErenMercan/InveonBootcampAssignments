using CourseInside.Models;

namespace CourseInside.Repositories
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByUserIdAsync(string userId);
        Task CreateCartAsync(Cart cart);
        Task AddCartItemAsync(CartItem cartItem);
        Task RemoveCartItemAsync(int cartItemId);
        Task SaveChangesAsync();
    }
}
