using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.AssigmentW1.Synchronous_Asynchronous
{
    internal class SynchronousRepository
    {
        private void RequestRepository()
        {
            Console.WriteLine("Information Requesting..");
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Information Fetched.");
        }

        private void PostRepository()
        {
            Console.WriteLine("Information Posting..");
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Information Posted..");
        }

        public void PostAndRequestRepository()
        {
            RequestRepository();
            PostRepository();
        }
    }
}
