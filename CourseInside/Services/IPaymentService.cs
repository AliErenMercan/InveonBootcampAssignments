using CourseInside.Models;

namespace CourseInside.Services
{
    public interface IPaymentService
    {
        Task<ServiceResult<IEnumerable<Payment>>> GetAllPaymentsAsync();
        Task<ServiceResult<Payment>> GetPaymentByIdAsync(int id);
        Task<ServiceResult> AddPaymentAsync(Payment payment);
        Task<ServiceResult> UpdatePaymentAsync(Payment payment);
        Task<ServiceResult> DeletePaymentAsync(int id);
    }
}
