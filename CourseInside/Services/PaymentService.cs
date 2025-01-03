using CourseInside.Models;
using CourseInside.Repositories;

namespace CourseInside.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IPaymentRepository repository, ILogger<PaymentService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ServiceResult<IEnumerable<Payment>>> GetAllPaymentsAsync()
        {
            var payments = await _repository.GetAllPaymentsAsync();
            return ServiceResult<IEnumerable<Payment>>.Success(payments);
        }

        public async Task<ServiceResult<Payment>> GetPaymentByIdAsync(int id)
        {
            var payment = await _repository.GetPaymentByIdAsync(id);
            if (payment == null)
            {
                return ServiceResult<Payment>.Failure("Payment not found");
            }

            return ServiceResult<Payment>.Success(payment);
        }

        public async Task<ServiceResult> AddPaymentAsync(Payment payment)
        {
            await _repository.AddPaymentAsync(payment);
            return ServiceResult.Success("Payment added successfully");
        }

        public async Task<ServiceResult> UpdatePaymentAsync(Payment payment)
        {
            await _repository.UpdatePaymentAsync(payment);
            return ServiceResult.Success("Payment updated successfully");
        }

        public async Task<ServiceResult> DeletePaymentAsync(int id)
        {
            await _repository.DeletePaymentAsync(id);
            return ServiceResult.Success("Payment deleted successfully");
        }
    }
}
