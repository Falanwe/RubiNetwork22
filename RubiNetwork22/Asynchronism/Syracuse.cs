using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asynchronism
{
    public static class Syracuse
    {
        private static long ComputeNext(long current)
        {
            if (current % 2 == 0)
            {
                return current / 2;
            }
            else
            {
                return 3 * current + 1;
            }
        }

        public static IEnumerable<long> Sequence(long seed)
        {
            var current = seed;
            while (true)
            {
                yield return current;
                current = ComputeNext(current);
            }
        }

        public static long MaxAltitude(long seed) => Sequence(seed).TakeWhile(val => val > 1).Max();
    }
}
