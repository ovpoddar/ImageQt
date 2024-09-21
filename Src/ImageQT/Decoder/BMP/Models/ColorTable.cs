using ImageQT.Decoder.BMP.Models.DIbFileHeader;
using ImageQT.Exceptions;
using ImageQT.Models.ImagqQT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP.Models;

internal readonly struct ColorTable
{
    private readonly Pixels[] _colors;
    public ColorTable(Stream fileStream, BMPHeader header)
    {
        // TODO: Doesnot support rle encoding of any kind
        var pixelsCount = header.BitDepth <= 8
               ? (0xff >> (8 - header.BitDepth)) + 1
               : header.ColorUsed;
        Span<byte> tableData = stackalloc byte[pixelsCount * header.CalculatePixelSize()];

        fileStream.Read(tableData);
        _colors = header.Type == BMPHeaderType.BitMapV5
            ? ReadWithLoop(tableData, pixelsCount)
            : MemoryMarshal.Cast<byte, Pixels>(tableData).ToArray();
    }

    private static Pixels[] ReadWithLoop(Span<byte> tableData, int pixelsCount)
    {
        var result = new Pixels[pixelsCount];

        for (int i = 0; i < pixelsCount; i++)
        {
            var index = i * 3;
            result[i] = new Pixels(tableData[index], tableData[index + 1], tableData[index + 2]);
        }

        return result;
    }

    public readonly Pixels this[int index]
    {
        get
        {
            if (index > _colors.Length) throw new InvalidOperationException();
            return _colors[index];
        }
    }

}
