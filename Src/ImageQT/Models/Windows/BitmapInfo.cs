﻿#if DEBUG || Windows
using System.Runtime.InteropServices;

namespace ImageQT.Models.Windows;
[StructLayout(LayoutKind.Sequential)]
public struct BitmapInfo
{
    public int biSize;
    public int biWidth;
    public int biHeight;
    public short biPlanes;
    public short biBitCount;
    public int biCompression;
    public int biSizeImage;
    public int biXPelsPerMeter;
    public int biYPelsPerMeter;
    public int biClrUsed;
    public int biClrImportant;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public uint[] bmiColors;
}
#endif