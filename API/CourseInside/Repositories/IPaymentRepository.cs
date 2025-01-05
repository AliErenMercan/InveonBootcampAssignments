using CourseInside.Models;

namespace CourseInside.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment?> GetByIdAsync(int id);
        Task AddAsync(Payment payment);
    }
}
