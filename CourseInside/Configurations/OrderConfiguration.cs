using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CourseInside.Models;

namespace CourseInside.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id); // Primary key

            builder.Property(o => o.OrderDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()");

            builder.HasOne(o => o.User)
                   .WithMany(u => u.Orders)
                   .HasForeignKey(o => o.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Course)
                   .WithMany(c => c.Orders)
                   .HasForeignKey(o => o.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Payment)
                   .WithOne(p => p.Order)
                   .HasForeignKey<Payment>(p => p.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
