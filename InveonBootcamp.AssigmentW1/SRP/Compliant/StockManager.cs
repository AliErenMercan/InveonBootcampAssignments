namespace InveonBootcamp.AssigmentW1.SRP.Compliant
{
    internal class StockManager
    {
        public void UpdateStock(string product, int quantity)
        {
            Console.WriteLine($"Stock updated: {product} - Remaining: {100 - quantity}");
        }
    }
}
