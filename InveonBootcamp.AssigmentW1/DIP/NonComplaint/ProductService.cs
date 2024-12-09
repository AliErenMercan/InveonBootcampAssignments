namespace InveonBootcamp.AssigmentW1.DIP.NonComplaint
{
    internal class ProductService
    {
        ProductRepository productRepository = new ProductRepository();
        public List<Product> GetAll()
        {
            var products = productRepository.GetAll();

            foreach (var product in products)
            {
                product.price = product.price * 1.2m;
            }

            return products;
        }
    }
}
