using ImageQT.Decoder.Formats.PNG.Models;
using ImageQT.Decoder.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.Formats.PNG;
internal class PngDecoder : IImageDecoder
{
    public int RequiredByteToRead => headerSignature.Length;

    private static ReadOnlySpan<byte> headerSignature =>
        [137, 80, 78, 71, 13, 10, 26, 10];

    public byte[] DecodeImage(Stream stream)
    {
        throw new NotImplementedException();
    }

    public (uint width, uint height, byte bitCount) GetImageDetails(Stream stream)
    {
        // skip png Signature 
        stream.Seek(headerSignature.Length, SeekOrigin.Begin);

        while (true)
        {
            var chunk = new PngChunk(stream);
            var chunkType = chunk.GetSignature();

            switch (chunkType)
            {
                case "IHDR":
                    var details = new IHDRHeader(chunk.GetData());
                    return (details.Width, details.Height, details.BitDepth);
                case "IEND":
                    throw new Exception("bad Image.");
                default:
                    continue;
            }
        }

    }

    public bool IsSupport(ReadOnlySpan<byte> header)
    {
        if (header == null) throw new ArgumentNullException("header");
        if (header.Length != RequiredByteToRead) throw new ArgumentNullException("header");
        return GenericHelper.Equal(header, headerSignature);
    }
}
