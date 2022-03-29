using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asynchronism
{
    public static class TcsSample
    {
        private static TaskCompletionSource _aPressedSource = new();

        private static async Task BWasPressed()
        {
            await _aPressedSource.Task;
            Console.WriteLine("A and B were both pressed");
        }

        public static void Run()
        {
            var shouldContinue = true;

            while (shouldContinue)
            {
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.A:
                        _aPressedSource.TrySetResult();
                        break;
                    case ConsoleKey.B:
                        _ = BWasPressed();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
