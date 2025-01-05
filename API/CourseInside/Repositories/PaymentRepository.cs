using CourseInside.Data;
using CourseInside.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseInside.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _context.Payments
                .Include(p => p.Order).ThenInclude(o => o.User)
                .Include(p => p.Order).ThenInclude(o => o.Course)
                .ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _context.Payments
                .Include(p => p.Order).ThenInclude(o => o.User)
                .Include(p => p.Order).ThenInclude(o => o.Course)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }
    }
}
