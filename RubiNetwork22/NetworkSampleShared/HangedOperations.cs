using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSampleShared
{
    [MessagePackEnum(SerializationMethod = EnumSerializationMethod.ByUnderlyingValue)]
    public enum HangedOperations : byte
    {
        State,
        Try
    }
}
