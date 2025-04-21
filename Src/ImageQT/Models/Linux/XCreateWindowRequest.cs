using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XCreateWindowRequest
{
    public byte RequestType;
    public byte Depth;
    public ushort Length;
    public uint Window;
    public uint Parent;
    public short X;
    public short Y;
    public ushort Width;
    public ushort Height;
    public ushort BorderWidth;
    public ushort Class;
    public uint Visual;
    public uint Mask;
}
