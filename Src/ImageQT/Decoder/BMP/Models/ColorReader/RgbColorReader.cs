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
    private readonly byte _redShift;
    private readonly byte _blueShift;
    private readonly byte _greenShift;
    private readonly byte _redMaskSize;
    private readonly byte _blueMaskSize;
    private readonly byte _greenMaskSize;
    private readonly byte _step;
    private readonly byte _mask;
    public RgbColorReader(Stream fileStream, BMPHeader RequiredProcessData, ColorTable? colorTable)
        : base(fileStream, RequiredProcessData)
    {
        _colorTable = colorTable;
        _redShift = CalculateMaskShift(ProcessData.RedMask);
        _blueShift = CalculateMaskShift(ProcessData.BlueMask);
        _greenShift = CalculateMaskShift(ProcessData.GreenMask);
        _redMaskSize = CalculateMaskSize(ProcessData.RedMask, _redShift);
        _blueMaskSize = CalculateMaskSize(ProcessData.BlueMask, _blueShift);
        _greenMaskSize = CalculateMaskSize(ProcessData.GreenMask, _greenShift);
        (_step, _mask) = GetDepthDetails();
    }

    internal override void Decode(ArraySegment<Pixels> result, Span<byte> pixel, ref int writingIndex)
    {
        switch (ProcessData.BitDepth)
        {
            case <= 8:
                {
                    Debug.Assert(pixel.Length == 1);
                    Debug.Assert(_colorTable.HasValue);
                    for (int j = _step; j >= 0; j -= ProcessData.BitDepth)
                    {
                        var currentBit = (byte)((pixel[0] & (byte)(_mask << j)) >> j);
                        if (writingIndex < ProcessData.Width)
                            result[writingIndex++] = _colorTable.Value[currentBit];
                    }

                    break;
                }

            case 16:
                {
                    Debug.Assert(pixel.Length == 2);
                    var value = BinaryPrimitives.ReadInt16LittleEndian(pixel);
                    var r = MapTo8Bits((byte)((value & ProcessData.RedMask) >> _redShift), _redMaskSize);
                    var g = MapTo8Bits((byte)((value & ProcessData.GreenMask) >> _greenShift), _greenMaskSize);
                    var b = MapTo8Bits((byte)((value & ProcessData.BlueMask) >> _blueShift), _blueMaskSize);
                    result[writingIndex++] = new Pixels(r, g, b);
                    break;
                }

            case 24:
                Debug.Assert(pixel.Length == 3);
                result[writingIndex++] = new Pixels(pixel[2], pixel[1], pixel[0]);
                break;
            case 32:
                /* TODO:ISSUE: according to Wikipidia 
                 * The 32-bit per pixel (32bpp) format supports 4,294,967,296 distinct colors and stores 1 pixel per 4-byte DWORD.
                 * Each DWORD can define the alpha, red, green and blue samples of the pixel.
                 * but found images in bgra format also can have MemoryMarshal.Cast if the format is bgra
                 * */
                Debug.Assert(pixel.Length == 4);
                result[writingIndex++] = new Pixels(pixel[2], pixel[1], pixel[0]);
                break;
        }
    }
}
