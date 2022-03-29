using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Asynchronism
{
    public static class TplSample
    {
        public static void Run()
        {
            var syncRoot = new object();
            var max = 0L;
            Parallel.For(100, 10_000_000, i =>
            {
                var maxAltitude = Syracuse.MaxAltitude(i);
                if (maxAltitude > max)
                {
                    lock (syncRoot)
                    {
                        if (maxAltitude > max)
                        {
                            max = maxAltitude;
                        }
                    }
                }
            });

            //for (var i = 100; i < 10_000_000; i++)
            //{
            //    ThreadPool.QueueUserWorkItem(_ =>
            //    {
            //        var maxAltitude = Syracuse.MaxAltitude(i);
            //        if (maxAltitude > max)
            //        {
            //            lock (syncRoot)
            //            {
            //                if (maxAltitude > max)
            //                {
            //                    max = maxAltitude;
            //                }
            //            }
            //        }
            //    });
            //}

            Console.WriteLine(max);
            Console.ReadLine();
        }
    }
}
