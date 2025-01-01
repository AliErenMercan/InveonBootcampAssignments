namespace CourseInside.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; } = 0m;
        public string PaymentStatus { get; set; } = "Pending";
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public Order Order { get; set; } = null!;
    }
}