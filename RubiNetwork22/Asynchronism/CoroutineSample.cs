using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Asynchronism
{
    public static class CoroutineSample
    {
        private static bool _programFinished = false;

        private static IEnumerator MyCoroutine()
        {
            Console.WriteLine("1");
            yield return null;
            Console.WriteLine("2");
            yield return null;
            yield return null;
            Console.WriteLine("3");
            yield return null;
            yield return null;
            yield return null;
            Console.WriteLine("4");
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            Console.WriteLine("Finished");
            _programFinished = true;
        }

        private static IEnumerator ShowSyracuse(long seed)
        {
            foreach(var val in Syracuse.Sequence(seed))
            {
                Console.WriteLine(val);
                yield return null;
            }
        }

        public static void Run()
        {
            var coroutineEngine = new CoroutineEngine();
            coroutineEngine.StartCoroutine(ShowSyracuse(873));

            while(!_programFinished)
            {
                Thread.Sleep(10);
            }

            coroutineEngine.Stop();
        }
    }
}
