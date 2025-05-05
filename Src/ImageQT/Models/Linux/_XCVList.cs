using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct _XCVList
{
    public IntPtr Cv;
    public IntPtr buf;
    public _XCVList* next;
}
