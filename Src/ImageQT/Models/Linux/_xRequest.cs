using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Linux;
[StructLayout(LayoutKind.Sequential)]
internal struct _xRequest
{
    public byte RequestType;
    public byte Data;
    public ushort Length;
}
