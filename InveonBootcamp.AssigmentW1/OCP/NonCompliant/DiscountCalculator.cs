namespace InveonBootcamp.AssigmentW1.OCP.NonCompliant
{
    internal class DiscountCalculator
    {
        public decimal CalculateDiscount(string discountType, decimal totalAmount)
        {
            decimal discountAmount = 0;
            switch (discountType)
            {
                case "Student":
                    discountAmount = totalAmount * 0.10m;
                    break;
                case "Seasonal":
                    discountAmount = totalAmount * 0.20m;
                    break;
                case "BlackFriday":
                    discountAmount = totalAmount * 0.30m;
                    break;
                default:
                    throw new ArgumentException("Unknown discount type");
            }
            return discountAmount;
        }
    }
}
