using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Asynchronism
{
    public static class ThreadPoolSample
    {
        public static void Run()
        {
            BigInteger result = 0;
            bool hasFinished = false;

            ThreadPool.QueueUserWorkItem(_ =>
            {
                result = BigCalculation.Last100DigitsOfFibo().ElementAt(100_000_000);
                hasFinished = true;
            });

            while (!hasFinished)
            {
                Console.WriteLine("computing...");
                Thread.Sleep(1000);
            }

            Console.WriteLine($"result is {result}");
        }

        public static void Tear()
        {
            BigStruct myStruct = default;

            bool shouldRun = true;

            ThreadPool.QueueUserWorkItem(_ =>
            {
                BigStruct struct1 = new BigStruct(1);
                while (shouldRun)
                {
                    myStruct = struct1;
                }
            });

            ThreadPool.QueueUserWorkItem(_ =>
            {
                BigStruct struct2 = new BigStruct(2);
                while (shouldRun)
                {
                    myStruct = struct2;
                }
            });

            ThreadPool.QueueUserWorkItem(_ =>
            {
                BigStruct struct3 = new BigStruct(3);
                while (shouldRun)
                {
                    myStruct = struct3;
                }
            });

            ThreadPool.QueueUserWorkItem(_ =>
            {
                BigStruct struct4 = new BigStruct(4);
                while (shouldRun)
                {
                    myStruct = struct4;
                }
            });

            while(!Console.KeyAvailable)
            {
                var copy = myStruct;
                if(copy.A != copy.B
                    || copy.B != copy.C
                    || copy.C != copy.D)
                {
                    Console.WriteLine($"something weird happenned: {copy.A} {copy.B} {copy.C} {copy.D}");
                    break;
                }
            }
            shouldRun = false;
        }
    }
}
