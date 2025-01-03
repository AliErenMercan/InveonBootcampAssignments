using CourseInside.Models;
using CourseInside.Repositories;

namespace CourseInside.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository repository, ILogger<OrderService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ServiceResult<IEnumerable<Order>>> GetAllOrdersAsync()
        {
            var orders = await _repository.GetAllOrdersAsync();
            return ServiceResult<IEnumerable<Order>>.Success(orders);
        }

        public async Task<ServiceResult<Order>> GetOrderByIdAsync(int id)
        {
            var order = await _repository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return ServiceResult<Order>.Failure("Order not found");
            }

            return ServiceResult<Order>.Success(order);
        }

        public async Task<ServiceResult> AddOrderAsync(Order order)
        {
            await _repository.AddOrderAsync(order);
            return ServiceResult.Success("Order added successfully");
        }

        public async Task<ServiceResult> UpdateOrderAsync(Order order)
        {
            await _repository.UpdateOrderAsync(order);
            return ServiceResult.Success("Order updated successfully");
        }

        public async Task<ServiceResult> DeleteOrderAsync(int id)
        {
            await _repository.DeleteOrderAsync(id);
            return ServiceResult.Success("Order deleted successfully");
        }
    }
}
