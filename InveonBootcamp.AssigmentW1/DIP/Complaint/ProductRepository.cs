namespace InveonBootcamp.AssigmentW1.DIP.Complaint
{
    internal class ProductRepository : IProductRepository
    {

        public List<Product> GetAll()
        {
            return
            [
                new Product() { id = 1, price = 100 },
                new Product() { id = 2, price = 200 },
                new Product() { id = 3, price = 300 }
            ];
        }

    }
}
