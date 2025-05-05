using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public struct XExtCodes
{
    public int Extension;      /* Extension number */
    public int MajorOpCode;   /* major op-code assigned by server */
    public int FirstEvent;    /* first event number for the Extension */
    public int FirstError;
}
