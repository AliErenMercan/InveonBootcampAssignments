using CourseInside.Models;
using CourseInside.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace CourseInside.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<OrderService> _logger;
        private readonly IMemoryCache _cache;

        public OrderService(
            IOrderRepository orderRepository,
            ICourseRepository courseRepository,
            IPaymentRepository paymentRepository,
            IUserRepository userRepository,
            ILogger<OrderService> logger,
            IMemoryCache cache)
        {
            _orderRepository = orderRepository;
            _courseRepository = courseRepository;
            _paymentRepository = paymentRepository;
            _userRepository = userRepository;
            _logger = logger;
            _cache = cache;
        }

        public async Task<ServiceResult<IEnumerable<Order>>> GetAllOrdersAsync()
        {
            if (!_cache.TryGetValue("AllOrders", out IEnumerable<Order> orders))
            {
                orders = await _orderRepository.GetAllOrdersAsync();
                var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                _cache.Set("AllOrders", orders, options);
            }
            return ServiceResult<IEnumerable<Order>>.Ok(orders);
        }

        public async Task<ServiceResult<Order>> GetOrderByIdAsync(int id)
        {
            var cacheKey = $"Order_{id}";
            if (!_cache.TryGetValue(cacheKey, out Order order))
            {
                order = await _orderRepository.GetByIdAsync(id);
                if (order == null)
                {
                    _logger.LogWarning("Order not found id={id}", id);
                    return ServiceResult<Order>.Fail("Sipariş bulunamadı!");
                }
                var options = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                _cache.Set(cacheKey, order, options);
            }
            return ServiceResult<Order>.Ok(order);
        }

        public async Task<ServiceResult> CreateOrderAsync(string userId, int courseId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("User not found userId={userId}", userId);
                return ServiceResult.Fail("Kullanıcı bulunamadı!");
            }
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null)
            {
                _logger.LogWarning("Course not found courseId={courseId}", courseId);
                return ServiceResult.Fail("Kurs bulunamadı!");
            }
            var newOrder = new Order
            {
                UserId = userId,
                CourseId = courseId,
                OrderDate = DateTime.UtcNow
            };
            await _orderRepository.AddAsync(newOrder);
            var payment = new Payment
            {
                OrderId = newOrder.Id,
                Amount = course.Price,
                PaymentStatus = "Completed",
                PaymentDate = DateTime.UtcNow
            };
            await _paymentRepository.AddAsync(payment);
            _logger.LogInformation("Order created orderId={orderId}, userId={userId}, courseId={courseId}", newOrder.Id, userId, courseId);
            _cache.Remove("AllOrders");
            return ServiceResult.Ok("Kurs başarıyla satın alındı.");
        }
    }
}
