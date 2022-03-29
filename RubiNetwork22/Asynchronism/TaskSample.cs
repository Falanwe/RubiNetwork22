using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Asynchronism
{
    public static class TaskSample
    {
        public static async IAsyncEnumerable<BigInteger> MakeSeriousCalculations()
        {
            var r1 = await Task.Run(() => BigCalculation.Last100DigitsOfFibo().ElementAt(9_999_999));
            yield return r1;
            yield return Syracuse.Sequence((long)(r1 % long.MaxValue)).TakeWhile(val => val != 1).LongCount();
        }

        public static Task<long> MakeSomeLongCalculation()
        {            
            return Task.Run(() => BigCalculation.Last100DigitsOfFibo().ElementAt(9_999_999))
                .ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        throw t.Exception;
                    }
                    else
                    {
                        var result1 = t.Result;
                        return Syracuse.Sequence((long)(result1 % long.MaxValue)).TakeWhile(val => val != 1).LongCount();
                    }
                });
        }

        public static async Task<long> MakeSomeLongCalculationWithAsync()
        {
            var result1 = await Task.Run(() => BigCalculation.Last100DigitsOfFibo().ElementAt(9_999_999));
            return Syracuse.Sequence((long)(result1 % long.MaxValue)).TakeWhile(val => val != 1).LongCount();
        }

        public static async Task Run()
        {
            await foreach(var val in MakeSeriousCalculations())
            {
                Console.WriteLine(val);
            }
        }
    }
}
