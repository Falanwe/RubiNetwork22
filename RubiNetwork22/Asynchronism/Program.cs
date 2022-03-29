using System;

namespace Asynchronism
{
    class Program
    {
        static void Main(string[] args)
        {
            AtomicityIssue.Run().Wait();
        }
    }
}
