using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.AssigmentW1.Synchronous_Asynchronous
{
    internal class TaskDefinitions
    {

        //Main thread içerisinde işlemler yapılırken farklı bir thread üzerinde bir işlem yaptırmak
        //istediğimiz zaman kullanılmaktadır.
        internal class TaskRunDefinition
        {
            public void MyTaskRun()
            {
                Console.WriteLine("Task1 started on this thread.");
                Task.Run(() =>
                {
                    Console.WriteLine("Task2 started on another thread.");
                    System.Threading.Thread.Sleep(100); //Some Processes
                    Console.WriteLine("Task2 finished on another thread.");
                });
                System.Threading.Thread.Sleep(20); //Some Processes
                Console.WriteLine("Task1 finished on this thread.");
            }
        }


        //System.Threading.Thread.Sleep metodundan farkli olarak threadi kitlemez asenkron olarak bekletme saglar.
        internal class TaskDelayDefinition
        {
            public async Task MyTaskDelay()
            {
                Console.WriteLine("Waiting for 2 seconds..");
                await Task.Delay(2000);
                Console.WriteLine("You can download now..");
            }
        }


        //eszamanli calistirilan birden fazla task'in hepsi tamamlanana dek donus saglamaz.
        //Tum tasklar tamamlandiginda sonuclarini List olarak dondurur.
        internal class TaskWhenAllDefinition
        {

            private async Task Task1()
            {
                Console.WriteLine("Task1 Started.");
                await Task.Delay(3000);
                Console.WriteLine("Task1 Completed.");
            }

            private async Task Task2()
            {
                Console.WriteLine("Task2 Started.");
                await Task.Delay(1000);
                Console.WriteLine("Task2 Completed.");
            }

            public async Task MyTaskWhenAll()
            {
                var task1 = Task1();
                var task2 = Task2();

                await Task.WhenAll(task1, task2);
                Console.WriteLine("All Tasks Completed.");
            }
        }

        //eszamanli calistirilan birden fazla task'in herhangi biri tamamlanana dek donus saglamaz.
        //herhangi biri tamamlandigi anda doner.
        internal class TaskWhenAnyDefinition
        {

            private async Task Task1()
            {
                Console.WriteLine("Task1 Started.");
                await Task.Delay(3000);
                Console.WriteLine("Task1 Completed.");
            }

            private async Task Task2()
            {
                Console.WriteLine("Task2 Started.");
                await Task.Delay(1000);
                Console.WriteLine("Task2 Completed.");
            }

            public async Task MyTaskWhenAny()
            {
                var task1 = Task1();
                var task2 = Task2();

                await Task.WhenAny(task1, task2);
                Console.WriteLine("Anyone of Tasks Completed.");
            }
        }
    }
}
