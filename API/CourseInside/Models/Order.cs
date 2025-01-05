namespace CourseInside.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public Payment Payment { get; set; } = null!;
    }
}
