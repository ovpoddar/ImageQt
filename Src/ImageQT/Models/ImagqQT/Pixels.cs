﻿using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace ImageQT.Models.ImagqQT;

[StructLayout(LayoutKind.Sequential, Pack = 0, Size = sizeof(byte) * 4)]
public struct Pixels
{
#if OSX
    public byte Alpha { get; set; }
    public byte Blue { get; set; }
    public byte Green { get; set; }
    public byte Red { get; set; }
#else
    public byte Blue { get; set; }
    public byte Green { get; set; }
    public byte Red { get; set; }
    public byte Alpha { get; set; }
#endif

    public Pixels(byte red, byte green, byte blue)
    {
        this.Red = red;
        this.Green = green;
        this.Blue = blue;
        this.Alpha = 0;
#if OSX
        this.Alpha = 255;
#endif
    }


    public Pixels(byte red, byte green, byte blue, byte alpha)
    {
        this.Red = red;
        this.Green = green;
        this.Blue = blue;
        this.Alpha = alpha;
    }

    public Pixels(int color, PixelFormat format)
    {
        if (color < 0)
            throw new ArgumentOutOfRangeException(nameof(color));

        Span<byte> bytes = stackalloc byte[4];

        if (BitConverter.IsLittleEndian)
            BinaryPrimitives.WriteInt32LittleEndian(bytes, color);
        else
            BinaryPrimitives.WriteInt32BigEndian(bytes, color);

        switch (format)
        {
            case PixelFormat.RRGGBBAA:
            case PixelFormat.RRGGBB:
                this.Red = bytes[0];
                this.Green = bytes[1];
                this.Blue = bytes[2];
                break;
            case PixelFormat.BBGGRRAA:
            case PixelFormat.BBGGRR:
                this.Blue = bytes[0];
                this.Green = bytes[1];
                this.Red = bytes[2];
                break;
            case PixelFormat.AARRGGBB:
                this.Red = bytes[1];
                this.Green = bytes[2];
                this.Blue = bytes[3];
                break;
            case PixelFormat.AABBGGRR:
                this.Blue = bytes[1];
                this.Green = bytes[2];
                this.Red = bytes[3];
                break;
        }

        this.Alpha = 0;
#if OSX
        this.Alpha = 255;
#endif
    }
}
