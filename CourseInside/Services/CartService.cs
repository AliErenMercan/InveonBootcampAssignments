using CourseInside.Data;
using CourseInside.Models;
using CourseInside.Repositories;

namespace CourseInside.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<CartService> _logger;

        public CartService(ICartRepository cartRepository,
                           ICourseRepository courseRepository,
                           ILogger<CartService> logger)
        {
            _cartRepository = cartRepository;
            _courseRepository = courseRepository;
            _logger = logger;
        }

        public async Task<ServiceResult<Cart>> GetOrCreateCartAsync(string userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                await _cartRepository.CreateCartAsync(cart);
            }
            return ServiceResult<Cart>.Success(cart);
        }

        public async Task<ServiceResult<Cart>> AddToCartAsync(string userId, int courseId, int quantity)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                await _cartRepository.CreateCartAsync(cart);
            }

            var course = await _courseRepository.GetCourseByIdAsync(courseId);
            if (course == null)
                return ServiceResult<Cart>.Failure("Course not found");

            var cartItem = new CartItem
            {
                CartId = cart.Id,
                CourseId = courseId,
                Quantity = quantity
            };
            await _cartRepository.AddCartItemAsync(cartItem);

            var updatedCart = await _cartRepository.GetCartByUserIdAsync(userId);
            return ServiceResult<Cart>.Success(updatedCart!);
        }

        public async Task<ServiceResult> RemoveFromCartAsync(int cartItemId)
        {
            await _cartRepository.RemoveCartItemAsync(cartItemId);
            return ServiceResult.Success("Item removed from cart");
        }

        public async Task<ServiceResult<Cart>> GetCartAsync(string userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
                return ServiceResult<Cart>.Failure("Cart not found");

            return ServiceResult<Cart>.Success(cart);
        }
    }
}
