using CourseInside.Configurations;
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
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CartItemConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());

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
            var courses = Enumerable.Range(1, 5).Select(i => new Course
            {
                Id = i,
                Title = $"Course {i}",
                Description = $"Description for Course {i}",
                Price = 100 + i * 10,
                Category = "Category A"
            }).ToArray();

            modelBuilder.Entity<Course>().HasData(courses);
        }
    }
}