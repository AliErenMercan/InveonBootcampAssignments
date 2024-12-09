using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.AssigmentW1.ISP.Compliant
{

    public interface IReadRepository
    {
        void GetProduct();
        void GetProduct(int id);
    }

    public interface IWriteRepository
    {
        void AddProduct();
        void DeleteProduct();
        void UpdateProduct();
    }

    public interface IContentProducerRepository
    {
        void AddProduct();
        void GetProduct();
        void GetProduct(int id);
    }

    public class AdminProductRepository : IReadRepository, IWriteRepository
    {
        public void AddProduct() { Console.WriteLine("Product Added"); }
        public void DeleteProduct() { Console.WriteLine("Product Deleted"); }
        public void UpdateProduct() { Console.WriteLine("Product Updated"); }
        public void GetProduct() { Console.WriteLine("Product Get"); }
        public void GetProduct(int id) { Console.WriteLine("Product Get by ID"); }
    }

    public class ClientProductRepository : IReadRepository
    {
        public void GetProduct() { Console.WriteLine("Product Get"); }
        public void GetProduct(int id) { Console.WriteLine("Product Get by ID"); }
    }

    public class ContentProducerRepository : IContentProducerRepository
    {
        public void AddProduct() { Console.WriteLine("Product Added"); }
        public void GetProduct() { Console.WriteLine("Product Get"); }
        public void GetProduct(int id) { Console.WriteLine("Product Get by ID"); }
    }
}
