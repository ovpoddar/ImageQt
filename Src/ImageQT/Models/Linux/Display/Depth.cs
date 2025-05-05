using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct Depth
{
    public int DepthValue; /* this depth (Z) of the depth */
    public int NVisuals; /* number of Visual types at this depth */
    public Visual* Visuals; /* list of visuals possible at this depth */
}