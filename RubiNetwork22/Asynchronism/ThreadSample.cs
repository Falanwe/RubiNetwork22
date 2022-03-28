using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Asynchronism
{
    public static class ThreadSample
    {     

        public static void Run()
        {
            BigInteger result=0;
            bool hasFinished = false;

            var thread = new Thread(() =>
            {
                result = BigCalculation.Last100DigitsOfFibo().ElementAt(100_000_000);
                hasFinished = true;
            });

            thread.Start();

            while(!hasFinished)
            {
                Console.WriteLine("computing...");
                Thread.Sleep(1000);
            }

            thread.Join();            

            Console.WriteLine($"result is {result}");
        }
    }
}
