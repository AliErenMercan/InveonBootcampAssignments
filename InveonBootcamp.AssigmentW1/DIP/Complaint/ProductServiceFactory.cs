using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.AssigmentW1.DIP.Complaint
{
    internal class ProductServiceFactory
    {
        private static IProductService? _instance;

        public IProductService CreateProductService(IProductRepository productRepository)
        {
            if(_instance is null)
            {
                _instance = new ProductService(productRepository);
            }
            return _instance;
        }

    }
}
