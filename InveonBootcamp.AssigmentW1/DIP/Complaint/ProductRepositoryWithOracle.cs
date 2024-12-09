using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.AssigmentW1.DIP.Complaint
{
    internal class ProductRepositoryWithOracle : IProductRepository
    {
        public List<Product> GetAll()
        {
            return
            [
                new Product() { id = 1, price = 1100 },
                new Product() { id = 2, price = 1200 },
                new Product() { id = 3, price = 1300 }
            ];
        }
    }
}
