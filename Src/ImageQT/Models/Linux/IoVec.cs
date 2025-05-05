using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public struct IoVec
{
    public IntPtr IOVBase; // Pointer to data
    public long IOVLength; // Length of data
}
