using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Asynchronism
{
    public static class BigCalculation
    {
        public static IEnumerable<BigInteger> Last100DigitsOfFibo()
        {


            BigInteger previous = 1;
            BigInteger current = 1;

            BigInteger aHundredDigits = BigInteger.Pow(10, 100);

            yield return previous;
            yield return current;
            long count = 2;

            while (true)
            {
                var next = previous + current % aHundredDigits;
                previous = current;
                current = next;
                count++;
                yield return current;
            }
        }
    }
}
