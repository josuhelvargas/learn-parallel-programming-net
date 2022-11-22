using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPrograming.Section1
{
    public static class WaitingTasks
    {
        public static void MainExecution()
        {
            //var cancellationToken = new CancellationToken();
            //var cancellationTokenRegistration = new CancellationTokenRegistration();
            
            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;


            var task1 = new Task(() =>
            {
                Console.WriteLine();


                for(int i= 0; i<5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(6000);
                }

                Console.WriteLine("Done!");
               
            }, token);

            task1.Start();
            task1.Wait(token);


            Task task2 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("Task 2 finished");
            }, token);

            //cancellationTokenSource.Cancel();

            Task.WaitAll(new[] { task1,task2 }, 10000, token );

            Console.WriteLine($"Task task1 is in status:{task1.Status}");
            Console.WriteLine($"Task task2 is in status:{task2.Status}");

            Console.WriteLine("Main program Done!");
            Console.ReadKey();
        }

    }
}
