// Ignore Spelling: Rgb

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
        switch (ProcessData.BitDepth)
        {
            case <= 8:
                {
                    Debug.Assert(pixel.Length == 1);
                    Debug.Assert(_colorTable.HasValue);
                    var details = GetDepthDetails();
                    for (int j = details.step; j >= 0; j -= ProcessData.BitDepth)
                    {
                        var currentBit = (byte)((pixel[0] & (byte)(details.mask << j)) >> j);
                        if (writingIndex < ProcessData.Width)
                            result[writingIndex++] = _colorTable.Value[currentBit];
                    }

                    break;
                }

            case 16:
                {
                    //TODO: CalculateMaskShift catch the value wither at base or to the header
                    Debug.Assert(pixel.Length == 2);
                    var value = BinaryPrimitives.ReadInt16LittleEndian(pixel);
                    var r = Map5BitsTo8Bits((byte)((value & ProcessData.RedMask) >> CalculateMaskShift(ProcessData.RedMask)));
                    var g = Map5BitsTo8Bits((byte)((value & ProcessData.GreenMask) >> CalculateMaskShift(ProcessData.GreenMask)));
                    var b = Map5BitsTo8Bits((byte)(value & ProcessData.BlueMask));
                    result[writingIndex++] = new Pixels(r, g, b);
                    break;
                }

            case 24:
                Debug.Assert(pixel.Length == 3);
                result[writingIndex++] = new Pixels(pixel[0], pixel[1], pixel[2]);
                break;
            case 32:
                Debug.Assert(pixel.Length == 4);
                // TODO:ERROR: according to https://en.wikipedia.org/wiki/BMP_file_format#Color_table
                // the format should be argb but found rgba 
                // verify the issue
                result[writingIndex++] = new Pixels(pixel[0], pixel[1], pixel[2]);
                break;
        }
    }
}
