namespace InveonBootcamp.AssigmentW1.OCP.Compliant
{
    internal class DiscountCalculator
    {

        public decimal CalculateDiscount(decimal totalAmount, IDiscountStrategy discountStrategy)
        {
            return discountStrategy.CalculateDiscount(totalAmount);
        }

        //Delegate Solution
        public decimal CalculateDiscountAsDelegate(decimal totalAmount, Func<decimal, decimal> func)
        {
            return func(totalAmount);
        }
    }

    public interface IDiscountStrategy
    {
        decimal CalculateDiscount(decimal totalAmount);
    }

    public class StudentDiscount : IDiscountStrategy
    {
        public decimal CalculateDiscount(decimal totalAmount)
        {
            return totalAmount * 0.10m;
        }
    }

    public class SeasonalDiscount : IDiscountStrategy
    {
        public decimal CalculateDiscount(decimal totalAmount)
        {
            return totalAmount * 0.20m;
        }
    }

    public class BlackFridayDiscount : IDiscountStrategy
    {
        public decimal CalculateDiscount(decimal totalAmount)
        {
            return totalAmount * 0.30m;
        }
    }
}
