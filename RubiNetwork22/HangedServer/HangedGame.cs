using MsgPack.Serialization;
using NetworkSampleShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HangedServer
{
    public class HangedGame
    {
        public string WordToGuess { get; set; }

        public string CurrentGuess { get; set; }

        public string MissingLetters { get; set; }

        private readonly List<TcpClient> _clients = new();

        public async Task SetupGame()
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://random-word-api.herokuapp.com/word");
            var responseString = await response.Content.ReadAsStringAsync();
            WordToGuess = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(responseString)[0];

            CurrentGuess = new string('_', WordToGuess.Length);
            MissingLetters = "";

            await SendStateToPlayers();
        }

        private async Task SendStateToPlayers()
        {
            TcpClient[] clientsCopy;
            lock (_clients)
            {
                clientsCopy = _clients.ToArray();
            }

            var actionSerializer = MessagePackSerializer.Get<HangedOperations>();
            var stateSerializer = MessagePackSerializer.Get<HangedState>();
            foreach (var client in clientsCopy)
            {
                try
                {
                    var stream = client.GetStream();
                    await actionSerializer.PackAsync(stream, HangedOperations.State);
                    await stateSerializer.PackAsync(stream, new HangedState { CurrentGuess = CurrentGuess, MissingLetters = MissingLetters });
                }
                //gotta catch'em all! (please don't do that in a real project)
                catch(Exception)
                {
                    lock(_clients)
                    {
                        _clients.Remove(client);
                    }
                }
            }
        }

        private async Task TryLetter(char c)
        {
            c = char.ToLowerInvariant(c);
            // unfinished... for now!
        }
    }
}
