namespace InveonBootcamp.AssigmentW1.DIP.NonComplaint
{
    internal class ProductController
    {
        private ProductService productService = new ProductService();

        public List<Product> GetAll()
        {
            return productService.GetAll();
        }



    }
}
