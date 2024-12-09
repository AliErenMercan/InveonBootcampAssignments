
namespace InveonBootcamp.AssigmentW1.Synchronous_Asynchronous
{
    internal class AsynchronousRepository
    {
        //Request result is important
        private async Task<bool> RequestRepository()
        {
            bool result = new Random().Next(100) < 50;
            Console.WriteLine("Information Requesting..");
            await Task.Delay(2500);
            if (result)
            {
                Console.WriteLine("Information Fetched.");
            }
            else
            {
                Console.WriteLine("Request Failure!");
            }
            
            return result;
        }

        //Posting result is not important
        private void PostRepository()
        {
            bool result = new Random().Next(100) < 50;
            Console.WriteLine("Information Posting..");
            Task.Delay(3000).ContinueWith(
                _ =>
                {
                    if (result)
                    {
                        Console.WriteLine("Information Posted..");
                    }
                    else
                    {
                        Console.WriteLine("Posting Failure!");
                    }
                });
        }


        public async Task PostAndRequestRepository()
        {
            PostRepository(); //result is not important
            bool result = await RequestRepository(); //result is important
            Console.WriteLine( (result) ? "Process Succesfully" : "Process Failure!");
        }
    }
}
