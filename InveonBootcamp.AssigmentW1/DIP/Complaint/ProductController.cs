namespace InveonBootcamp.AssigmentW1.DIP.Complaint
{
    internal class ProductController
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public List<Product> GetAll()
        {
            return _productService.GetAll();
        }



    }
}
