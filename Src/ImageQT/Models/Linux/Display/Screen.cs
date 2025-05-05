using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct Screen
{
    public XExtData* ExtData; // XExtData*
    public XDisplay* Display; // XDisplay*
    public ulong Root; // Window (XID, usually ulong)
    public int Width; // screen width in pixels
    public int Height; // screen height in pixels
    public int MWidth; // width in millimeters
    public int MHeight; // height in millimeters
    public int NDepths; // number of supported depths
    public Depth* Depths; // Depth* (list of depths)
    public int RootDepth; // bits per pixel
    public Visual* RootVisual; // Visual*
    public ulong DefaultGC; // GC (Graphics Context)
    public ulong CMap; // Colormap
    public ulong WhitePixel; // pixel value for white
    public ulong BlackPixel; // pixel value for black
    public int MaxMaps; // max colormaps
    public int MinMaps; // min colormaps
    public int BackingStore; // enum: Never, WhenMapped, Always
    public int SaveUnders; // Bool (0 or non-zero)
    public long RootInputMask; // input event mask
}