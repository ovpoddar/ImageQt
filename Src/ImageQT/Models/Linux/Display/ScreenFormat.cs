using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct ScreenFormat
{
    public XExtData* ExtData; /* hook for extension to hang data */
    public int Depth; /* depth of this image format */
    public int BitsPerPixel; /* bits/pixel at this depth */
    public int ScanLinePad; /* scanline must padded to this multiple */
}