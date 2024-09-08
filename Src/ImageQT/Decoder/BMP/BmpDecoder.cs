// Ignore Spelling: Bmp

using ImageQT.Decoder.BMP.Models;
using ImageQT.Decoder.BMP.Models.ColorReader;
using ImageQT.Decoder.BMP.Models.DIbFileHeader;
using ImageQT.Decoder.Helpers;
using ImageQT.Models.ImagqQT;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;


namespace ImageQT.Decoder.BMP;
internal class BmpDecoder : IImageDecoder
{
    private readonly Stream _fileStream;
    private static ReadOnlySpan<byte> BMHeaderSignature => [66, 77];
    private static ReadOnlySpan<byte> BAHeaderSignature => [66, 65];

    public BmpDecoder(Stream stream) =>
        _fileStream = stream;

    public bool CanProcess()
    {
        _fileStream.Position = 0;
        var byteRequiredToVerify = Math.Max(BMHeaderSignature.Length, BAHeaderSignature.Length);
        Span<byte> signature = stackalloc byte[byteRequiredToVerify];
        _fileStream.Read(signature);
        return GenericHelper.Equal(signature, BMHeaderSignature) || GenericHelper.Equal(BAHeaderSignature, signature);
    }

    public Image Decode()
    {
        //1, 4, 8, 16, 24, 32
        Pixels[] result;
        int height, width;
        BMPHeader header;
        _fileStream.Position = 0;
        if (BitConverter.IsLittleEndian)
        {
            var fileHeader = _fileStream.FromStream<BmpFileHeader>();
            header = new BMPHeader(_fileStream);
            height = header.GetNormalizeHeight();
            width = header.Width < 0 ? header.Width * -1 : header.Width;
            result = new Pixels[width * height];

            _fileStream.Seek(fileHeader.OffsetData, SeekOrigin.Begin);
        }
        else
        {
            // not sure do i need this or not
            // TODO: check on arm processor if the BitConverter.IsLittleEndian is false and how it react
            height = 0; width = 0; result = []; header = default;
        }
        ProcessImage(result, header);
        return ImageLoader.LoadImage(width, height, ref result);
    }

    private void ProcessImage(Pixels[] result, BMPHeader header)
    {
        var currentPos = _fileStream.Position;
        var reader = GetReader(header);
        var rowWithPadding = reader.CalculationOfRowSize();
        var height = header.GetNormalizeHeight();
        Span<byte> pixel = stackalloc byte[header.GetMinimumPixelsSizeInByte()];
        ArraySegment<Pixels> writingSection;

        for (var i = 0; i < height; i++)
        {
            var writingIndex = 0;
            _fileStream.Seek(i * rowWithPadding + currentPos, SeekOrigin.Begin);
            writingSection = new ArraySegment<Pixels>(result, GetWritingOffset(i, header), header.Width);
            while (writingIndex < header.Width)
            {
                _fileStream.ReadExactly(pixel);
                reader.Decode(writingSection, pixel, ref writingIndex);
            }
        }
    }

    private static int GetWritingOffset(int i, BMPHeader header)
    {
        var maxTarget = header.GetNormalizeHeight() - 1;
        return header.Height < 0
            ? i * header.Width
            : (maxTarget - i) * header.Width;
    }

    private BaseColorReader GetReader(BMPHeader header) =>
        header.Compression switch
        {
            HeaderCompression.Rgb => new RgbColorReader(_fileStream, header),
            _ => throw new NotImplementedException()
        };

}


