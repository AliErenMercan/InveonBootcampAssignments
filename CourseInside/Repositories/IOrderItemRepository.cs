using CourseInside.Models;

namespace CourseInside.Repositories
{
    public interface IOrderItemRepository
    {
        Task AddOrderItemAsync(OrderItem orderItem);
    }
}
