using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace ImageQT.Decoder.Models;

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
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = 0;
#if OSX
        this.Alpha = 255;
#endif
    }


    public Pixels(byte red, byte green, byte blue, byte alpha)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
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
                Red = bytes[0];
                Green = bytes[1];
                Blue = bytes[2];
                break;
            case PixelFormat.BBGGRRAA:
            case PixelFormat.BBGGRR:
                Blue = bytes[0];
                Green = bytes[1];
                Red = bytes[2];
                break;
            case PixelFormat.AARRGGBB:
                Red = bytes[1];
                Green = bytes[2];
                Blue = bytes[3];
                break;
            case PixelFormat.AABBGGRR:
                Blue = bytes[1];
                Green = bytes[2];
                Red = bytes[3];
                break;
        }

        Alpha = 0;
#if OSX
        this.Alpha = 255;
#endif
    }
}
