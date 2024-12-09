using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.AssigmentW1.ISP.NonCompliant
{
    public interface IProductRepository
    {
        void AddProduct();
        void DeleteProduct();
        void UpdateProduct();
        void GetProduct();
        void GetProduct(int id);
    }

    public class ProductRepository : IProductRepository
    {
        public void AddProduct()        { Console.WriteLine("Product Added"); }
        public void DeleteProduct()     { Console.WriteLine("Product Deleted"); }
        public void UpdateProduct()     { Console.WriteLine("Product Updated"); }
        public void GetProduct()        { Console.WriteLine("Product Get"); }
        public void GetProduct(int id)  { Console.WriteLine("Product Get by ID"); }
    }
}
