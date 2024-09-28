using ImageQT.Exceptions;
using ImageQT.Models.ImagqQT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP.Models.ColorReader;
internal abstract class BaseColorReader
{
    protected BMPHeader HeaderDetails { get; }

    protected BaseColorReader(BMPHeader header) =>
        this.HeaderDetails = header;

    internal abstract void Decode(ArraySegment<Pixels> result, Span<byte> pixel, ref int writingIndex);

    public int CalculationOfRowSize()
    {
        var s1 = Math.Ceiling((HeaderDetails.BitDepth * HeaderDetails.Width) / 32D) * 4;
        var s2 = Math.Floor((HeaderDetails.BitDepth * HeaderDetails.Width + 31D) / 32) * 4;
        Debug.Assert(s1 == s2);
        return (int)s1;
    }

    protected (byte step, byte mask) GetDepthDetails()
    {
        byte step = 1;
        for (byte i = 0; i < HeaderDetails.BitDepth; i++)
            step |= (byte)(1 << i);

        return ((byte)(8 - HeaderDetails.BitDepth), step);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static byte CalculateMaskShift(long value)
    {
        // TODO:TEST check on arm processor if the BitConverter.IsLittleEndian is false and how it react
        // not sure do i need this or not
        byte result = 0;
        if (value == 0)
            return result;

        while (true)
        {
            if ((value & 1) != 1)
            {
                value >>= 1;
                result++;
            }
            else
            {
                break;
            }
        }
        return result;
    }

    protected static byte CalculateMaskSize(long value, byte shift)
    {
        byte result = 0;
        value >>= shift;
        while (true)
        {
            if ((value & 1) != 0)
            {
                value >>= 1;
                result++;
            }
            else
            {
                break;
            }
        }
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static byte MapTo8Bits(long value, int bitCount) => bitCount switch
    {
        var x when x < 8 && x > 4 => (byte)((value << (8 - bitCount)) | (value << (8 - (8 - bitCount)))),
        8 => (byte)(value),
        var x when x > 8 => (byte)(value >> (bitCount - 8)),
        _ => throw new ArgumentException("Unsupported bit count"),
    };

}

