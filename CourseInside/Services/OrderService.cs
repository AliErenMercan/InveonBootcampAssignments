using CourseInside.Data;
using CourseInside.Models;
using CourseInside.Repositories;

namespace CourseInside.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ICartService _cartService;
        private readonly AppDbContext _dbContext;
        private readonly ILogger<OrderService> _logger;

        public OrderService(
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            IPaymentRepository paymentRepository,
            ICartRepository cartRepository,
            ICartService cartService,
            AppDbContext dbContext,
            ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _paymentRepository = paymentRepository;
            _cartRepository = cartRepository;
            _cartService = cartService;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<ServiceResult<IEnumerable<Order>>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return ServiceResult<IEnumerable<Order>>.Success(orders);
        }

        public async Task<ServiceResult<Order>> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return ServiceResult<Order>.Failure("Order not found");
            }

            return ServiceResult<Order>.Success(order);
        }

        public async Task<ServiceResult> AddOrderAsync(Order order)
        {
            await _orderRepository.AddOrderAsync(order);
            return ServiceResult.Success("Order added successfully");
        }

        public async Task<ServiceResult> UpdateOrderAsync(Order order)
        {
            await _orderRepository.UpdateOrderAsync(order);
            return ServiceResult.Success("Order updated successfully");
        }

        public async Task<ServiceResult> DeleteOrderAsync(int id)
        {
            await _orderRepository.DeleteOrderAsync(id);
            return ServiceResult.Success("Order deleted successfully");
        }

        public async Task<ServiceResult<decimal>> CheckoutCartAsync(string userId)
        {
            // Transaction başlat
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                // 1) Sepeti çek
                var cartResult = await _cartService.GetCartAsync(userId);
                if (!cartResult.success || cartResult.data == null)
                {
                    return ServiceResult<decimal>.Failure("Cart not found or empty");
                }
                var cart = cartResult.data;
                if (!cart.CartItems.Any())
                {
                    return ServiceResult<decimal>.Failure("Cart is empty");
                }

                // 2) Yeni Order oluştur
                var newOrder = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.UtcNow
                };
                await _orderRepository.AddOrderAsync(newOrder); // SaveChanges() çağrılıyor

                decimal totalAmount = 0m;

                // 3) Her CartItem -> OrderItem
                foreach (var cartItem in cart.CartItems)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = newOrder.Id,
                        CourseId = cartItem.CourseId,
                        UnitPrice = cartItem.Course.Price,
                        Quantity = cartItem.Quantity
                    };
                    await _orderItemRepository.AddOrderItemAsync(orderItem); // SaveChanges()
                    totalAmount += orderItem.UnitPrice * orderItem.Quantity;
                }

                // 4) Payment kaydı
                var payment = new Payment
                {
                    OrderId = newOrder.Id,
                    Amount = totalAmount,
                    PaymentStatus = "Completed",
                    PaymentDate = DateTime.UtcNow
                };
                await _paymentRepository.AddPaymentAsync(payment); // SaveChanges()

                // 5) Sepeti temizleyelim (sil cartItems)
                foreach (var item in cart.CartItems.ToList())
                {
                    await _cartRepository.RemoveCartItemAsync(item.Id); // SaveChanges()
                }

                // 6) Transaction commit
                await transaction.CommitAsync();

                _logger.LogInformation("Checkout succeeded for user {UserId}, order {OrderId}, total {Amount}",
                    userId, newOrder.Id, totalAmount);

                return ServiceResult<decimal>.Success(totalAmount, "Checkout completed successfully");
            }
            catch (Exception ex)
            {
                // Hata varsa rollback
                await transaction.RollbackAsync();

                _logger.LogError(ex, "Checkout FAILED for user {UserId}", userId);
                return ServiceResult<decimal>.Failure("Checkout failed: " + ex.Message);
            }
        }
    }
}
