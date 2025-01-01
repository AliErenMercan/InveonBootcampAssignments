using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CourseInside.Models;

namespace CourseInside.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id); // Primary key

            builder.Property(p => p.Amount)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(p => p.PaymentStatus)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.PaymentDate)
                   .IsRequired();

            builder.HasOne(p => p.Order)
                   .WithOne(o => o.Payment)
                   .HasForeignKey<Payment>(p => p.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
