using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asynchronism
{
    public static class EventSample
    {

        private static event Action _onAPressed;
        private static event Action _onBPressed;
        private static event Action _onCPressed;


        public static void Run()
        {
            var shouldContinue = true;
            _onAPressed += EventSample__onAPressed;
            _onBPressed += () => Console.WriteLine("B was pressed");
            _onCPressed += () =>
            {
                Console.WriteLine("C was pressed");
                shouldContinue = false;            };


            while(shouldContinue)
            {
                var key = Console.ReadKey();
                switch(key.Key)
                {
                    case ConsoleKey.A:
                        _onAPressed?.Invoke();
                        _onAPressed -= EventSample__onAPressed;
                        break;
                    case ConsoleKey.B:
                        _onBPressed?.Invoke();
                        break;
                    case ConsoleKey.C:
                        _onCPressed?.Invoke();
                        break;
                    default:
                        break;
                }
            }
        }

        private static void EventSample__onAPressed()
        {
            Console.WriteLine("A was pressed");
        }
    }
}
