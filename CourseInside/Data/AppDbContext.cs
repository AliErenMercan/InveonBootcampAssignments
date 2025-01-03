using CourseInside.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CourseInside.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<Order>()
               .HasOne(o => o.User)
               .WithMany(u => u.Orders)
               .HasForeignKey(o => o.UserId)
               .IsRequired();

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Course)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CourseId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payment>(p => p.OrderId);



            // Seed Users
            var adminUser = new User
            {
                Id = "1",
                UserName = "admin@example.com",
                Email = "admin@example.com",
                Name = "Admin User",
                Role = "Admin",
                PasswordHash = new PasswordHasher<User>().HashPassword(new User(), "Admin123!")
            };

            var teacherUser = new User
            {
                Id = "2",
                UserName = "teacher@example.com",
                Email = "teacher@example.com",
                Name = "Teacher User",
                Role = "Teacher",
                PasswordHash = new PasswordHasher<User>().HashPassword(new User(), "Teacher123!")
            };

            var regularUser = new User
            {
                Id = "3",
                UserName = "user@example.com",
                Email = "user@example.com",
                Name = "Regular User",
                Role = "User",
                PasswordHash = new PasswordHasher<User>().HashPassword(new User(), "User123!")
            };

            modelBuilder.Entity<User>().HasData(adminUser, teacherUser, regularUser);

            // Seed Courses
            var courses = Enumerable.Range(1, 15).Select(i => new Course
            {
                Id = i,
                Title = $"Course {i}",
                Description = $"Description for Course {i}",
                Price = 100 + i * 10,
                Category = "Category A"
            }).ToArray();

            modelBuilder.Entity<Course>().HasData(courses);

            // Seed Orders and Payments
            var orders = new List<Order>
            {
                new Order { Id = 1, UserId = "1", CourseId = 1, OrderDate = DateTime.UtcNow },
                new Order { Id = 2, UserId = "1", CourseId = 2, OrderDate = DateTime.UtcNow },
                new Order { Id = 3, UserId = "1", CourseId = 3, OrderDate = DateTime.UtcNow },

                new Order { Id = 4, UserId = "3", CourseId = 4, OrderDate = DateTime.UtcNow },
                new Order { Id = 5, UserId = "3", CourseId = 5, OrderDate = DateTime.UtcNow },
                new Order { Id = 6, UserId = "3", CourseId = 6, OrderDate = DateTime.UtcNow },
                new Order { Id = 7, UserId = "3", CourseId = 7, OrderDate = DateTime.UtcNow },
                new Order { Id = 8, UserId = "3", CourseId = 8, OrderDate = DateTime.UtcNow }
            };

            modelBuilder.Entity<Order>().HasData(orders);

            var payments = orders.Select(o => new Payment
            {
                Id = o.Id,
                OrderId = o.Id,
                Amount = courses.First(c => c.Id == o.CourseId).Price,
                PaymentStatus = "Completed",
                PaymentDate = DateTime.UtcNow
            }).ToArray();

            modelBuilder.Entity<Payment>().HasData(payments);
        }
    }
}