using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public struct XExtCodes
{
    public int Extension; /* Extension number */
    public int MajorOpCode; /* major op-code assigned by server */
    public int FirstEvent; /* first event number for the Extension */
    public int FirstError;
}