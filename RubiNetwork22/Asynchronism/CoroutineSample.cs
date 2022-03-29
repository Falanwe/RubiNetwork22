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

        public static void Run()
        {
            var coroutineEngine = new CoroutineEngine();
            coroutineEngine.StartCoroutine(MyCoroutine());

            while(!_programFinished)
            {
                Thread.Sleep(10);
            }

            coroutineEngine.Stop();
        }
    }
}
