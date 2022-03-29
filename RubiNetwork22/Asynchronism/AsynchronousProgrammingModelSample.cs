using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asynchronism
{
    public static class AsynchronousProgrammingModelSample
    {
        public static void Run()
        {
            using var stream = File.OpenRead("Bidule.txt");

            var buffer = new byte[256];
            var shouldWait = true;

            stream.BeginRead(buffer, 0, 256, asyncResult =>
            {                
                var bytesRead = stream.EndRead(asyncResult);
                shouldWait = false;
                Console.WriteLine($"I read {bytesRead} bytes in the file");                
            }, null);

            while(shouldWait)
            {
                Console.WriteLine("waiting...");
            }
        }
    }
}
