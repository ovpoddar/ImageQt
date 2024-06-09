using ImageQT.Decoder.Formats.BMP.Models;
using ImageQT.Decoder.Helpers;
using System.Runtime.InteropServices;
namespace ImageQT.Decoder.Formats.BMP;
internal class BMPDecoder : IImageDecoder
{
    public int RequiredByteToRead => sizeof(ushort);

    public byte[] DecodeImage(Stream stream)
    {
        throw new NotImplementedException();
    }

    public (uint width, uint height, byte bitCount) GetImageDetails(Stream stream)
    {
        var fileHeaderSize = Marshal.SizeOf<BmpFileHeader>();

        if (stream.Length <= fileHeaderSize)
            throw new Exception("bad Image.");

        var details = stream.FromStream<DibFileHeader>(fileHeaderSize, SeekOrigin.Begin);
        return ((uint)details.Width, (uint)details.Height, (byte)details.BitCount);
    }

    public bool IsSupport(Span<byte> header)
    {
        if (header == null) throw new ArgumentNullException("header");
        if (header.Length != RequiredByteToRead) throw new ArgumentNullException("header");
        return header.ToStruct<ushort>() == 0x4D42;
    }
}
