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
    public ColorTable(BMPHeader header, Span<byte> tableData)
    {
        _colors = header.CalculatePixelSize() switch
        {
            3 => ReadWithLoop(tableData),
            4 => MemoryMarshal.Cast<byte, Pixels>(tableData).ToArray(),
            _ => throw new NotImplementedException(),
        };
    }

    private static Pixels[] ReadWithLoop(Span<byte> tableData)
    {
        Debug.Assert(tableData.Length % 3 == 0);
        var pixelCount = tableData.Length / 3;
        var result = new Pixels[pixelCount];

        for (var i = 0; i < pixelCount; i++)
        {
            result[i] = new Pixels(tableData[i * 3],
                tableData[i * 3 + 1],
                tableData[i * 3 + 2]);
        }

        return result;
    }

    public readonly Pixels this[int index]
    {
        get
        {
            if (index > _colors.Length) 
                throw new InvalidOperationException($"{index} is access voilation {_colors.Length}");
            return _colors[index];
        }
    }

}
