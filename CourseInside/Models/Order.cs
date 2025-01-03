
namespace CourseInside.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;
        public Course Course { get; set; } = null!;
        public Payment Payment { get; set; } = null!;
    }
}
