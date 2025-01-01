using System;

namespace CourseInside.Models
{
    public class Course
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}