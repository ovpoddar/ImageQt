using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XCBGenericEvent
{
    public byte ResponseType;
    public byte Pad0;
    public ushort Sequence;
    public fixed uint Pad[7];
    public uint FullSequence;
}
