namespace InveonBootcamp.AssigmentW1.SRP.NonCompliant
{
    internal class OrderProcessing
    {
        public void CreateOrder(string product, int quantity)
        {
            Console.WriteLine($"Order created: {product} - Quantity: {quantity}");
            UpdateStock(product, quantity);
            SendOrderConfirmationEmail(product, quantity);
            GenerateOrderReport(product, quantity);
        }

        private void UpdateStock(string product, int quantity)
        {
            Console.WriteLine($"Stock updated: {product} - Remaining: {100 - quantity}");
        }

        private void SendOrderConfirmationEmail(string product, int quantity)
        {
            Console.WriteLine($"Email sent for order: {product} - Quantity: {quantity}");
        }

        private void GenerateOrderReport(string product, int quantity)
        {
            Console.WriteLine($"Report generated for order: {product} - Quantity: {quantity}");
        }
    }
}
