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
    public Stream FileStream { get; }
    public BMPHeader ProcessData { get; }

    protected BaseColorReader(Stream fileStream, BMPHeader requiredProcessData)
    {
        this.FileStream = fileStream;
        this.ProcessData = requiredProcessData;
    }

    internal abstract void Decode(ArraySegment<Pixels> result, Span<byte> pixel, ref int writingIndex);

    public int CalculationOfRowSize()
    {
        var s1 = Math.Ceiling((ProcessData.BitDepth * ProcessData.Width) / 32D) * 4;
        var s2 = Math.Floor((ProcessData.BitDepth * ProcessData.Width + 31D) / 32) * 4;
        Debug.Assert(s1 == s2);
        return (int)s1;
    }

    protected (byte step, byte mask) GetDepthDetails()
    {
        byte step = 1;
        for (byte i = 0; i < ProcessData.BitDepth; i++)
            step |= (byte)(1 << i);

        return ((byte)(8 - ProcessData.BitDepth), step);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected byte Map5BitsTo8Bits(byte value)
    {
        return (byte)((value * 8) + (value >> 2) + (value >> 5));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected byte Map6BitsTo8Bits(byte value)
    {
        return (byte)((value * 4) + (value >> 4));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static byte CalculateMaskShift(long value)
    {
        // TODO: check on arm processor if the BitConverter.IsLittleEndian is false and how it react
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected byte MapSmallerBitsTo8Bits(byte maximumLimit, byte value)
    {
        return (byte)(byte.MaxValue / maximumLimit * value);
    }
}

