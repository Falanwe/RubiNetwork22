using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Asynchronism
{
    public class CoroutineEngine
    {
        public bool _isRunning = true;

        private readonly List<IEnumerator> _enumerators = new();

        public CoroutineEngine()
        {
            ThreadPool.QueueUserWorkItem(Run);
        }

        private void ExecuteOneStep()
        {
            var index = 0;
            while(index < _enumerators.Count)
            {
                if(!_enumerators[index].MoveNext())
                {
                    lock (_enumerators)
                    {
                        _enumerators.RemoveAt(index);
                    }
                }
                else
                {
                    index++;
                }
            }
        }

        private void Run(object? _)
        {
            while(_isRunning)
            {
                ExecuteOneStep();
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public void StartCoroutine(IEnumerator coroutine)
        {
            lock (_enumerators)
            {
                _enumerators.Add(coroutine);
            }
        }
    }
}
