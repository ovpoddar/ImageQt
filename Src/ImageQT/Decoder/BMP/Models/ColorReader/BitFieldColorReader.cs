using ImageQT.Decoder.BMP.Models.DIbFileHeader;
using ImageQT.Models.ImagqQT;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP.Models.ColorReader;
internal class BitFieldColorReader : BaseColorReader
{
    public BitFieldColorReader(Stream fileStream, BMPHeader RequiredProcessData)
      : base(fileStream, RequiredProcessData) {}

    internal override void Decode(ArraySegment<Pixels> result, Span<byte> pixel, ref int writingIndex)
    {
        // TODO: found a image with palate no doc found
        // 16
        if (ProcessData.BitDepth == 16)
        {
            var value = BinaryPrimitives.ReadInt16LittleEndian(pixel);
            var r = (byte)(((value & ProcessData.RedMask) >> 11) * 8.2); // TODO: multiply not getting the exact amount due to the decimal find another way.
            var g = (byte)(((value & ProcessData.GreenMask) >> 5) * 4);
            var b = (byte)((value & ProcessData.BlueMask) * 8.2);
            result[writingIndex++] = new Pixels(r, g, b);
        }
        // 32
        if (ProcessData.BitDepth == 24)
        {
            result[writingIndex++] = new Pixels(pixel[0], pixel[1], pixel[2]);
        }
    }
}
