// Ignore Spelling: Bmp

using ImageQT.Decoder.BMP.Models;
using ImageQT.Decoder.BMP.Models.ColorReader;
using ImageQT.Decoder.BMP.Models.DIbFileHeader;
using ImageQT.Decoder.Helpers;
using ImageQT.Models.ImagqQT;
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
        RequiredProcessData header;
        _fileStream.Position = 0;
        if (BitConverter.IsLittleEndian)
        {
            var fileHeader = _fileStream.FromStream<BmpFileHeader>();
            header = ReadBmpHeader(_fileStream);
            height = header.Height < 0 ? header.Height * -1 : header.Height;
            width = header.Width < 0 ? header.Width * -1 : header.Width;
            result = new Pixels[width * height];

            _fileStream.Seek(fileHeader.OffsetData, SeekOrigin.Begin);
        }
        else
        {
            // not sure do i need this or not
            // TODO: chack on arm processer if the BitConverter.IsLittleEndian is false and how it react
            height = 0; width = 0; result = []; header = default;
        }
        ProcessImage(result, header, _fileStream);
        return ImageLoader.LoadImage(width, height, ref result);
    }

    private void ProcessImage(Pixels[] result, RequiredProcessData header, Stream fileStream)
    {
        var reader = GetReader(header, fileStream);
        reader.Decode(result);
    }

    private static RequiredProcessData ReadBmpHeader(Stream stream)
    {
        var sizeofBMPHeaderType = Marshal.SizeOf(Enum.GetUnderlyingType(typeof(BMPHeaderType))); ;
        Span<byte> bmpHeaderType = stackalloc byte[sizeofBMPHeaderType];
        stream.Read(bmpHeaderType);
        switch (bmpHeaderType.ToStruct<BMPHeaderType>())
        {
            case BMPHeaderType.BitMapCore:
                return stream.FromStream<BitMapCoreHeader>(-sizeofBMPHeaderType)
                    .GetPropertyValue();
            case BMPHeaderType.OS22XBitMapSmall:
                return stream.FromStream<Os22xBitMapHeaderSmall>(-sizeofBMPHeaderType)
                    .GetPropertyValue();
            case BMPHeaderType.BitMapINFO:
                return stream.FromStream<BitMapInfoHeader>(-sizeofBMPHeaderType)
                    .GetPropertyValue();
            case BMPHeaderType.BitMapV2INFO:
                return stream.FromStream<BitMapV2InfoHeader>(-sizeofBMPHeaderType)
                    .GetPropertyValue();
            case BMPHeaderType.BitMapV3INFO:
                return stream.FromStream<BitMapV3InfoHeader>(-sizeofBMPHeaderType)
                    .GetPropertyValue();
            case BMPHeaderType.OS22XBitMap:
                return stream.FromStream<Os22xBitMapHeader>(-sizeofBMPHeaderType)
                    .GetPropertyValue();
            case BMPHeaderType.BitMapV4:
                return stream.FromStream<BitMapV4Header>(-sizeofBMPHeaderType)
                    .GetPropertyValue();
            case BMPHeaderType.BitMapV5:
                return stream.FromStream<BitMapV5Header>(-sizeofBMPHeaderType)
                    .GetPropertyValue();
            default:
                break;
        }
        throw new NotImplementedException();
    }

    private BaseColorReader GetReader(RequiredProcessData header, Stream fileStream) =>
        header.comperssion switch
        {
            HeaderCompression.Rgb => new RgbColorReader(fileStream, header),
            _ => throw new NotImplementedException()
        };

}


