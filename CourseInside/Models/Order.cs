
namespace CourseInside.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public User User { get; set; } = null!;
        public Course Course { get; set; } = null!;
        public Payment Payment { get; set; } = null!;
    }
}
