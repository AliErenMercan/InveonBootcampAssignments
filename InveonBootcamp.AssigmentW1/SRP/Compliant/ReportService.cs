namespace InveonBootcamp.AssigmentW1.SRP.Compliant
{
    internal class ReportService
    {
        public void GenerateOrderReport(string product, int quantity)
        {
            Console.WriteLine($"Report generated for order: {product} - Quantity: {quantity}");
        }
    }
}
