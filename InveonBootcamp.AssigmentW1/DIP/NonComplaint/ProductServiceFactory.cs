using InveonBootcamp.AssigmentW1.DIP.Complaint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.AssigmentW1.DIP.NonComplaint
{
    internal class ProductServiceFactory
    {
        public ProductService CreateProductService()
        {
            return new ProductService();
        }
    }
}
