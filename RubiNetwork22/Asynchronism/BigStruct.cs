using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asynchronism
{
    public struct BigStruct
    {
        public BigStruct(long l)
        {
            A = l;
            B = l;
            C = l;
            D = l;
        }

        public long A { get; }
        public long B { get; }
        public long C { get; }
        public long D { get; }
    }
}
