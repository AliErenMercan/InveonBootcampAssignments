namespace InveonBootcamp.AssigmentW1.SRP.Compliant
{
    internal class EmailService
    {
        public void SendOrderConfirmationEmail(string product, int quantity)
        {
            Console.WriteLine($"Email sent for order: {product} - Quantity: {quantity}");
        }
    }
}
