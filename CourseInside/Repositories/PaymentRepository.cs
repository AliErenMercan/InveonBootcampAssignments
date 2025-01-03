using CourseInside.Data;
using CourseInside.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                .Include(p => p.Order)
                .ThenInclude(o => o.User)
                .Include(p => p.Order)
                .ThenInclude(o => o.Course)
                .ToListAsync();
        }

        public async Task<Payment?> GetPaymentByIdAsync(int id)
        {
            return await _context.Payments
                .Include(p => p.Order)
                .ThenInclude(o => o.User)
                .Include(p => p.Order)
                .ThenInclude(o => o.Course)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePaymentAsync(int id)
        {
            var payment = await GetPaymentByIdAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
