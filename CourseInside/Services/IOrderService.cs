using CourseInside.Models;

namespace CourseInside.Services
{
    public interface IOrderService
    {
        Task<ServiceResult<IEnumerable<Order>>> GetAllOrdersAsync();
        Task<ServiceResult<Order>> GetOrderByIdAsync(int id);
        Task<ServiceResult> AddOrderAsync(Order order);
        Task<ServiceResult> UpdateOrderAsync(Order order);
        Task<ServiceResult> DeleteOrderAsync(int id);
    }
}
