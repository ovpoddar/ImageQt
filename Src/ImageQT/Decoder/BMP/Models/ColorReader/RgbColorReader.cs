﻿// Ignore Spelling: Rgb

using ImageQT.Decoder.BMP.Models.DIbFileHeader;
using ImageQT.Models.ImagqQT;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP.Models.ColorReader;
internal class RgbColorReader : BaseColorReader
{
    private readonly ColorTable? _colorTable;

    public RgbColorReader(Stream fileStream, BMPHeader RequiredProcessData, ColorTable? colorTable)
        : base(fileStream, RequiredProcessData) => _colorTable = colorTable;

    internal override void Decode(ArraySegment<Pixels> result, Span<byte> pixel, ref int writingIndex)
    {
        if (this.ProcessData.BitDepth <= 8)
        {
            Debug.Assert(pixel.Length == 1);
            Debug.Assert(_colorTable.HasValue);
            var details = GetDepthDetails();
            for (int j = details.step; j >= 0; j -= ProcessData.BitDepth)
            {
                var mask = (byte)(details.mask << j);
                var currentBit = (byte)((pixel[0] & mask) >> j);
                if (writingIndex < ProcessData.Width)
                    result[writingIndex++] = _colorTable.Value[currentBit];
            }
        }
        else if (this.ProcessData.BitDepth == 16)
        {
            Debug.Assert(pixel.Length == 2);
            var value = BinaryPrimitives.ReadInt16LittleEndian(pixel); // work on both endien
            var r = base.ProcessData.Compression == HeaderCompression.BitFields
                ? (byte)(((value & 0b1111100000000000) >> 11) * 17) // TODO verify
                : (byte)(((value & 0b01111100000000000) >> 10) * 8.2);
            var g = base.ProcessData.Compression == HeaderCompression.BitFields
                ? (byte)(((value & 0b0000011111100000) >> 5) * 17)
                : (byte)(((value & 0b00000001111100000) >> 5) * 8.2);
            var b = base.ProcessData.Compression == HeaderCompression.BitFields
                ? (byte)(((value & 0b0000000000011111)) * 17)
                : (byte)(((value & 0b00000000000011111)) * 8.2);
            result[writingIndex++] = new Pixels(r, g, b);
        }
        else if (this.ProcessData.BitDepth == 24)
        {
            Debug.Assert(pixel.Length == 3);
            result[writingIndex++] = new Pixels(pixel[0], pixel[1], pixel[2]);
        }
        else if (this.ProcessData.BitDepth == 32)
        {
            Debug.Assert(pixel.Length == 4);
            // ERROR: according to https://en.wikipedia.org/wiki/BMP_file_format#Color_table
            // the format should be argb but found rgba 
            // verify the issue
            result[writingIndex++] = new Pixels(pixel[0], pixel[1], pixel[2]);
        }
    }
}
