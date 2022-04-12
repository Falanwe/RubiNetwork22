using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkSampleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var client = new UdpClient();

            var inputString = @" _______________________________________
|\ ___________________________________ /|
| | _                               _ | |
| |(+)        _           _        (+)| |
| | ~      _--/           \--_      ~ | |
| |       /  /             \  \       | |
| |      /  |               |  \      | |
| |     /   |               |   \     | |
| |     |   |    _______    |   |     | |
| |     |   |    \     /    |   |     | |
| |     \    \_   |   |   _/    /     | |
| |      \     -__|   |__-     /      | |
| |       \_                 _/       | |
| |         --__         __--         | |
| |             --|   |--             | |
| |               |   |               | |
| |                | |                | |
| |                 |                 | |
| |                                   | |
| |   I S   G O O D   F O R   Y O U   | |
| | _                               _ | |
| |(+)                             (+)| |
| | ~                               ~ | |
|/ ----------------------------------- \|
 --------------------------------------- ";


            var dataToSend = Encoding.UTF8.GetBytes(inputString);
            while (true)
            {
                Console.WriteLine("press Enter to send data, press C to close");

                bool shouldContinue = false;
                while(!shouldContinue)
                {
                    var pressed = Console.ReadKey();
                    if(pressed.Key == ConsoleKey.C)
                    {
                        return;
                    }
                    if(pressed.Key == ConsoleKey.Enter)
                    {
                        shouldContinue = true;
                    }
                }

                await client.SendAsync(dataToSend, dataToSend.Length, "localhost", 666);
                var response = await client.ReceiveAsync();
                var outputString = Encoding.UTF8.GetString(response.Buffer);

                Console.WriteLine("I received a response:");
                Console.WriteLine(outputString);
            }
        }
    }
}
