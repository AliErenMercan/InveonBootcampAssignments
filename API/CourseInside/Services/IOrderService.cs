using CourseInside.Models;

namespace CourseInside.Services
{
    public interface IOrderService
    {
        Task<ServiceResult<IEnumerable<Order>>> GetAllOrdersAsync();
        Task<ServiceResult<Order>> GetOrderByIdAsync(int id);
        Task<ServiceResult> CreateOrderAsync(string userId, int courseId);
    }
}
