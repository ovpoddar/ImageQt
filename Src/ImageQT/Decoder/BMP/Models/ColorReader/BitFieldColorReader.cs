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
        // 16
        if (ProcessData.BitDepth == 16)
        {
            var value = BinaryPrimitives.ReadInt16LittleEndian(pixel);
            // TODO: make dynamic calculation 
            // TODO: CalculateMaskShift catch the value either at base or to the header
            var r = Map5BitsTo8Bits((byte)(((value & ProcessData.RedMask) >> CalculateMaskShift(ProcessData.RedMask))));
            var g = Map6BitsTo8Bits((byte)(((value & ProcessData.GreenMask) >> CalculateMaskShift(ProcessData.GreenMask))));
            var b = Map5BitsTo8Bits((byte)((value & ProcessData.BlueMask)));
            result[writingIndex++] = new Pixels(r, g, b);
        }
        // 32
        if (ProcessData.BitDepth == 24)
        {
            result[writingIndex++] = new Pixels(pixel[0], pixel[1], pixel[2]);
        }
    }
}