using CourseInside.Data;
using CourseInside.Models;
using CourseInside.RabbitMQ;
using CourseInside.Repositories;

namespace CourseInside.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<CartService> _logger;
        private readonly QueueManager _queueManager;

        public CartService(ICartRepository cartRepository,
                           ICourseRepository courseRepository,
                           ILogger<CartService> logger,
                           QueueManager queueManager)
        {
            _cartRepository = cartRepository;
            _courseRepository = courseRepository;
            _logger = logger;
            _queueManager = queueManager;
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

            // Aynı kursun var olup olmadığını kontrol edelim
            var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.CourseId == courseId);
            if (existingCartItem != null)
            {
                // Notification oluştur ve RabbitMQ'ya gönder
                var notificationMessage = new
                {
                    UserId = userId,
                    Message = $"The course '{course.Title}' is already in your cart."
                };
                _queueManager.PublishMessage("notification_exchange", "", notificationMessage);

                return ServiceResult<Cart>.Failure("This course is already in your cart.");
            }

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
