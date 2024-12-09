using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.AssigmentW1.DIP.Complaint
{
    internal interface IProductService
    {
        List<Product> GetAll();
    }
}
