using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordleWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordleController : Controller
    {
        private readonly IWordleService _wordle;

        public WordleController(IWordleService wordle)
        {
            _wordle = wordle;
        }

        [HttpGet("RandomWord")]
        public string RandomWord()
        {
            return _wordle.GetRandomWord();
        }
    }
}
