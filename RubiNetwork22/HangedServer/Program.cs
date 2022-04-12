using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace HangedServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var listener = new TcpListener(IPAddress.Any, 666);
            var cts = new CancellationTokenSource();

            var startedTcs = new TaskCompletionSource();
            Console.WriteLine("Press C to close the server");
            async Task WaitForCancellation()
            {
                await startedTcs.Task;
                while (Console.ReadKey().Key != ConsoleKey.C)
                {
                }
                cts.Cancel();
                listener.Stop();
            }

            _ = WaitForCancellation();

            listener.Start();
            startedTcs.SetResult();

            while(!cts.Token.IsCancellationRequested)
            {
                var client = await listener.AcceptTcpClientAsync();
                
            }
        }
    }
}
