using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Asynchronism
{
    public static class ContinuationSample
    {
        public static void ComputeBigNumber(Action<BigInteger> continuation)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                var result = BigCalculation.Last100DigitsOfFibo().ElementAt(100_000_000);
                continuation(result);
            });
        }

        public static void Run()
        {
            var shouldContinue = true;

            ComputeBigNumber(bigNumber =>
            {
                Console.WriteLine($"I found a big number: {bigNumber}");
                shouldContinue = false;
            });

            for(var i = 1; shouldContinue; i++)
            {
                Console.WriteLine(i);
            }
        }
    }
}
