using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WordleWebApi
{
    public class WordleService : IWordleService
    {
        private readonly Random _random = new();
        private readonly List<string> _validWords = new();

        public void Init()
        {
            StreamReader file = null;
            try
            {
                file = File.OpenText("fr_5words.txt");

                for (var word = file.ReadLine(); word != null; word = file.ReadLine())
                {
                    _validWords.Add(word);
                }
            }
            finally
            {
                file?.Dispose();
            }
        }

        public string GetRandomWord() => _validWords[_random.Next(_validWords.Count)];
    }
}
