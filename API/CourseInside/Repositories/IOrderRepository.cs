using CourseInside.Models;

namespace CourseInside.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetByIdAsync(int id);
        Task AddAsync(Order order);
    }
}
