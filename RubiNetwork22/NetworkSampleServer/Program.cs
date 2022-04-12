using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkSampleServer
{
    class Program
    {
        private const string _forbiddenCharacters = "|+~";

        static async Task Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var cancellationTask = Task.Run(() =>
            {
                while(Console.ReadKey().Key != ConsoleKey.C)
                {
                }
                cts.Cancel();
            });

            using var server = new UdpClient(666);

            while (!cts.Token.IsCancellationRequested)
            {
                try
                {
                    await await Task.WhenAny(ServeRequest(server), WaitforCancellation(cts.Token));
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Server shutting down");
                }
            }

            await cancellationTask;

            static async Task WaitforCancellation(CancellationToken token)
            {
                while(true)
                {
                    token.ThrowIfCancellationRequested();
                    await Task.Delay(100);
                }
            }

            static async Task ServeRequest(UdpClient server)
            {
                var receiveResult = await server.ReceiveAsync();

                var data = receiveResult.Buffer;
                var inputString = Encoding.UTF8.GetString(data);

                Console.WriteLine("I received some input from a client:");
                Console.WriteLine(inputString);

                var outputString = inputString;
                foreach (var c in _forbiddenCharacters)
                {
                    outputString = outputString.Replace(c, '@');
                }

                var remoteEndpoint = receiveResult.RemoteEndPoint;

                var dataToSend = Encoding.UTF8.GetBytes(outputString);
                await server.SendAsync(dataToSend, dataToSend.Length, remoteEndpoint);
            }

            Console.WriteLine("Server closed");
            Console.ReadLine();
        }
    }
}
