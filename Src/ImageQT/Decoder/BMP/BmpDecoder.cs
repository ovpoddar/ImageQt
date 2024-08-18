// Ignore Spelling: Bmp

using ImageQT.Decoder.BMP.Models;
using ImageQT.Decoder.BMP.Models.DIbFileHeader;
using ImageQT.Decoder.Helpers;
using ImageQT.Models.ImagqQT;
using ImageQT.Models.Windows;
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
        _fileStream.Position = 0;
        if (BitConverter.IsLittleEndian)
        {
            var fileHeader = _fileStream.FromStream<BmpFileHeader>();
            //switch
        }
        else
        {
            var c = new BitMapCoreHeader();
            
        }
        return default;
    }

    private T ReadBmpHeader<T>(Stream stream) where T : struct
    {
        var sizeofBMPHeaderType = Marshal.SizeOf<BMPHeaderType>();
        var bmpHeaderType = _fileStream.FromStream<BMPHeaderType>();
        switch (bmpHeaderType)
        {
            case BMPHeaderType.BitMapCore:
                return (T)(object)_fileStream.FromStream<BitMapCoreHeader>(-sizeofBMPHeaderType);
            case BMPHeaderType.OS22XBitMapSmall:
                return (T)(object)_fileStream.FromStream<Os22xBitMapHeaderSmall>(-sizeofBMPHeaderType);
            case BMPHeaderType.BitMapINFO:
                return (T)(object)_fileStream.FromStream<BitMapInfoHeader>(-sizeofBMPHeaderType);
            case BMPHeaderType.BitMapV2INFO:
                return (T)(object)_fileStream.FromStream<BitMapV2InfoHeader>(-sizeofBMPHeaderType);
            case BMPHeaderType.BitMapV3INFO:
                return (T)(object)_fileStream.FromStream<BitMapV3InfoHeader>(-sizeofBMPHeaderType);
            case BMPHeaderType.OS22XBitMap:
                return (T)(object)_fileStream.FromStream<Os22xBitMapHeader>(-sizeofBMPHeaderType);
            case BMPHeaderType.BitMapV4:
                return (T)(object)_fileStream.FromStream<BitMapV4Header>(-sizeofBMPHeaderType);
            case BMPHeaderType.BitMapV5:
                return (T)(object)_fileStream.FromStream<BitMapV5Header>(-sizeofBMPHeaderType);
            default:
                break;
        }
        throw new NotImplementedException();
    }
}


