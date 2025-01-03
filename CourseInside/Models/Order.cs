using System;

namespace CourseInside.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;
        public Payment Payment { get; set; } = null!;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
