namespace InveonBootcamp.AssigmentW1.DIP.Complaint
{
    internal class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetAll()
        {
            var products = _productRepository.GetAll();

            foreach (var product in products)
            {
                product.price = product.price * 1.2m;
            }

            return products;
        }
    }
}
