// Ignore Spelling: Rgb

using ImageQT.Decoder.BMP.Models.DIbFileHeader;
using ImageQT.Models.ImagqQT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP.Models.ColorReader;
internal class RgbColorReader : BaseColorReader
{
    public RgbColorReader(Stream fileStream, BMPHeader RequiredProcessData)
        : base(fileStream, RequiredProcessData) { } 

    internal override void Decode(ArraySegment<Pixels> result, Span<byte> pixel, ref int writingIndex)
    {
        if (this.ProcessData.BitDepth < 8)
        {
            throw new NotImplementedException();
        }
        else if (this.ProcessData.BitDepth == 8)
        {
            Debug.Assert(pixel.Length == 1);
            result[writingIndex++] = new Pixels(pixel[0], pixel[0], pixel[0]);
        }   
        else if (this.ProcessData.BitDepth == 16)
        {
            Debug.Assert(pixel.Length == 2);
            throw new NotImplementedException();
            // result[writingIndex++] = new Pixels(pixel[0], pixel[1], pixel[1]);
        }
        else if (this.ProcessData.BitDepth == 24)
        {
            Debug.Assert(pixel.Length == 3);
            result[writingIndex++] = new Pixels(pixel[0], pixel[1], pixel[2]);
        }
        else if (this.ProcessData.BitDepth == 32)
        {
            Debug.Assert(pixel.Length == 4);
            result[writingIndex++] = new Pixels(pixel[1], pixel[2], pixel[3]);
        }
    }
}
