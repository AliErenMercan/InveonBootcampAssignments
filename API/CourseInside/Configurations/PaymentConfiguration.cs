using CourseInside.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseInside.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Amount).HasPrecision(18, 2).IsRequired();
            builder.Property(p => p.PaymentStatus).HasMaxLength(50).IsRequired();
            builder.Property(p => p.PaymentDate).IsRequired();
        }
    }
}
