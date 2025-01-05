using CourseInside.Models;
using CourseInside.Repositories;

namespace CourseInside.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IPaymentRepository paymentRepository, ILogger<PaymentService> logger)
        {
            _paymentRepository = paymentRepository;
            _logger = logger;
        }

        public async Task<ServiceResult<IEnumerable<Payment>>> GetAllPaymentsAsync()
        {
            var payments = await _paymentRepository.GetAllPaymentsAsync();
            _logger.LogInformation("Found payments count={count}", payments.Count());
            return ServiceResult<IEnumerable<Payment>>.Ok(payments);
        }

        public async Task<ServiceResult<Payment>> GetPaymentByIdAsync(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
            {
                _logger.LogWarning("Payment not found id={id}", id);
                return ServiceResult<Payment>.Fail("Ödeme Bulunamadı!");
            }
            return ServiceResult<Payment>.Ok(payment);
        }
    }
}
