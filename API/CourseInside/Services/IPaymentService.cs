using CourseInside.Models;

namespace CourseInside.Services
{
    public interface IPaymentService
    {
        Task<ServiceResult<IEnumerable<Payment>>> GetAllPaymentsAsync();
        Task<ServiceResult<Payment>> GetPaymentByIdAsync(int id);
    }
}
