using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.ImagqQT;

[StructLayout(LayoutKind.Sequential, Pack = 0, Size = sizeof(byte) * 4)]
public struct Pixels
{
    public byte Blue { get; set; }
    public byte Green { get; set; }
    public byte Red { get; set; }
    public byte Alfa { get; set; }

    public Pixels(byte red, byte green, byte blue)
    {
        this.Red = red;
        this.Green = green;
        this.Blue = blue;
#if OSX
        this.Alfa = 255;
#endif
        this.Alfa = 0;
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

#if OSX
        this.Alfa = 255;
#else
        this.Alfa = 0;
#endif
    }
}
