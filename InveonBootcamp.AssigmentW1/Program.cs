
using InveonBootcamp.AssigmentW1.SRP.Compliant;
using InveonBootcamp.AssigmentW1.OCP.Compliant;
using InveonBootcamp.AssigmentW1.LSP.Compliant;
using InveonBootcamp.AssigmentW1.ISP.Compliant;
using InveonBootcamp.AssigmentW1.DIP.Complaint;
using InveonBootcamp.AssigmentW1.Synchronous_Asynchronous;
using static InveonBootcamp.AssigmentW1.Synchronous_Asynchronous.TaskDefinitions;



//SRP
Console.WriteLine("");
Console.WriteLine("SRP Example Outputs");
Console.WriteLine("-------------------");

StockManager stockManager = new StockManager();
EmailService emailService = new EmailService();
ReportService reportService = new ReportService();
OrderController orderController = new OrderController(stockManager, emailService, reportService);
orderController.CreateOrder("Jewelry", 25);


//OCP
Console.WriteLine("");
Console.WriteLine("OCP Example Outputs");
Console.WriteLine("-------------------");

DiscountCalculator discountCalculator = new DiscountCalculator();
decimal discountForStudents = discountCalculator.CalculateDiscount(100, new StudentDiscount());
Console.WriteLine($"Discount For Student: {discountForStudents}");

static decimal CustomDiscount(decimal totalAmount)
{
    return totalAmount * 0.5m;
}
decimal discountForSpecials = discountCalculator.CalculateDiscountAsDelegate(100, CustomDiscount);
Console.WriteLine($"Discount For Specials: {discountForSpecials}");




//LSP
Console.WriteLine("");
Console.WriteLine("LSP Example Outputs");
Console.WriteLine("-------------------");

Phone phone = new Iphone();
phone.Call();
if(phone is ITakePhoto iphoneTakePhoto)
{
    iphoneTakePhoto.TakePhoto();
}

phone = new Nokia();
phone.Call();
if (phone is ITakePhoto nokiaTakePhoto)
{
    nokiaTakePhoto.TakePhoto();
}

if (phone is IPlaySnake nokiaPlaySnake)
{
    nokiaPlaySnake.PlaySnake();
}


//ISP
Console.WriteLine("");
Console.WriteLine("ISP Example Outputs");
Console.WriteLine("-------------------");

AdminProductRepository adminProductRepository = new AdminProductRepository();
Console.WriteLine("Admin Processes:");
adminProductRepository.AddProduct();
adminProductRepository.DeleteProduct();
adminProductRepository.UpdateProduct();
adminProductRepository.GetProduct();

ClientProductRepository clientProductRepository = new ClientProductRepository();
Console.WriteLine("Client Processes:");
clientProductRepository.GetProduct();

ContentProducerRepository contentProducerRepository = new ContentProducerRepository();
Console.WriteLine("Content Producer Processes:");
contentProducerRepository.AddProduct();
contentProducerRepository.GetProduct();



//DIP
Console.WriteLine("");
Console.WriteLine("DIP Example Outputs");
Console.WriteLine("-------------------");

ProductServiceFactory productServiceFactory = new ProductServiceFactory();
ProductController productController = new ProductController(productServiceFactory.CreateProductService(new ProductRepository()));
//roductController productControllerWithOracle = new ProductController(productServiceFactory.CreateProductService(new ProductRepositoryWithOracle()));
List<Product> products = productController.GetAll();
foreach (Product product in products)
{
    Console.WriteLine($"ID: {product.id}, Price: {product.price}");
}




//Synchronous-Asynchronous
Console.WriteLine("");
Console.WriteLine("Synchronous Example Outputs");
Console.WriteLine("-------------------");
SynchronousRepository synchronousRepository = new SynchronousRepository();
synchronousRepository.PostAndRequestRepository();

Console.WriteLine("");
Console.WriteLine("Asynchronous Example Outputs");
Console.WriteLine("-------------------");
AsynchronousRepository asynchronousRepository = new AsynchronousRepository();
await asynchronousRepository.PostAndRequestRepository();


Console.Write("Press any key to continue.."); Console.ReadKey();

//Task Definitions
Console.WriteLine("");
Console.WriteLine("Task Run Example Outputs");
Console.WriteLine("-------------------");
TaskRunDefinition taskRunDefinition = new TaskRunDefinition();
taskRunDefinition.MyTaskRun();


Console.WriteLine("");
Console.WriteLine("Task Delay Example Outputs");
Console.WriteLine("-------------------");
TaskDelayDefinition taskDelayDefinition = new TaskDelayDefinition();
await taskDelayDefinition.MyTaskDelay();

Console.WriteLine("");
Console.WriteLine("Task WhenAll Example Outputs");
Console.WriteLine("-------------------");
TaskWhenAllDefinition taskWhenAllDefinition = new TaskWhenAllDefinition();
await taskWhenAllDefinition.MyTaskWhenAll();

Console.WriteLine("");
Console.WriteLine("Task WhenAny Example Outputs");
Console.WriteLine("-------------------");
TaskWhenAnyDefinition taskWhenAnyDefinition = new TaskWhenAnyDefinition();
await taskWhenAnyDefinition.MyTaskWhenAny();












Console.ReadKey();







