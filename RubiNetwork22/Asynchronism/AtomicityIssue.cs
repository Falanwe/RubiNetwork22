using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Asynchronism
{
    public static class AtomicityIssue
    {
        public static async Task Run()
        {

            var count = 0;
            var syncRoot = new object();
            void IncrementCount()
            {
                for (var i = 0; i < 100_000_000; i++)
                {
                    Interlocked.Increment(ref count);
                }
            }

            void IncrementCountByTwo()
            {
                int currentCount;
                int newCount;
                for (var i = 0; i < 100_000_000; i++)
                {
                    do
                    {
                        currentCount = count;
                        newCount = count + 2;
                    }
                    while (Interlocked.CompareExchange(ref count, newCount, currentCount) != currentCount);
                }
            }

            var watch = new Stopwatch();
            watch.Start();

            var t1 = Task.Run(IncrementCountByTwo);
            var t2 = Task.Run(IncrementCountByTwo);
            var t3 = Task.Run(IncrementCountByTwo);
            var t4 = Task.Run(IncrementCountByTwo);

            await t1;
            await t2;
            await t3;
            await t4;

            Console.WriteLine(count);
            Console.WriteLine($"it took {watch.ElapsedMilliseconds} ms");
        }
    }
}
