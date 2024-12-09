namespace InveonBootcamp.AssigmentW1.SRP.Compliant
{
    internal class OrderController
    {
        private readonly StockManager _stockManager;
        private readonly EmailService _emailService;
        private readonly ReportService _reportService;

        public OrderController(StockManager stockManager, EmailService emailService, ReportService reportService)
        {
            _stockManager = stockManager;
            _emailService = emailService;
            _reportService = reportService;
        }

        public void CreateOrder(string product, int quantity)
        {
            Console.WriteLine($"Order created: {product} - Quantity: {quantity}");
            _stockManager.UpdateStock(product, quantity);
            _emailService.SendOrderConfirmationEmail(product, quantity);
            _reportService.GenerateOrderReport(product, quantity);
        }
    }
}
