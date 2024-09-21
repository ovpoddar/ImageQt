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
}

