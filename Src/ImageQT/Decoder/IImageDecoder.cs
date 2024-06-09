using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder;
internal interface IImageDecoder
{
    int RequiredByteToRead { get; }
    byte[] DecodeImage(Stream stream);
    bool IsSupport(ReadOnlySpan<byte> header);
    (uint width, uint height, byte bitCount) GetImageDetails(Stream stream);
}
