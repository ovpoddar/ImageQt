// Ignore Spelling: Bmp

using ImageQT.Decoder.BMP.Models;
using ImageQT.Decoder.BMP.Models.ColorReader;
using ImageQT.Decoder.BMP.Models.DIbFileHeader;
using ImageQT.Decoder.BMP.Models.Feature;
using ImageQT.Decoder.Helpers;
using ImageQT.Exceptions;
using ImageQT.Models.ImagqQT;
using System.Diagnostics;


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
        Pixels[] result;
        int height, width;
        BMPHeader header;
        ColorTable? colorTable = null;
        _fileStream.Position = 0;
        if (BitConverter.IsLittleEndian)
        {
            var fileHeader = _fileStream.FromStream<BmpFileHeader>();
            header = new BMPHeader(_fileStream);
            height = header.GetNormalizeHeight();
            width = header.Width < 0 ? header.Width * -1 : header.Width;
            result = new Pixels[width * height];

            if (fileHeader.OffsetData != _fileStream.Position)
            {
                var availableByte = (int)(fileHeader.OffsetData - _fileStream.Position);
                var extraBitMasksSize = header.CalculateTheSizeOfExtraBitMask(availableByte);
                if (extraBitMasksSize.HasValue)
                {
                    Span<byte> ExtraBit = stackalloc byte[extraBitMasksSize.Value];
                    _fileStream.Read(ExtraBit);
                    header.ReadFromExtraBits(ExtraBit);

                }
                availableByte = (int)(fileHeader.OffsetData - _fileStream.Position);
                var colorTableSize = header.CalculateDetailsOfPalate(availableByte);
                if (colorTableSize.HasValue)
                {
                    Span<byte> colorPalate = stackalloc byte[colorTableSize.Value];
                    _fileStream.Read(colorPalate);
                    colorTable = new ColorTable(header, colorPalate);
                }
            }
            _fileStream.Position = fileHeader.OffsetData;
        }
        else
        {
            // TODO:TEST: check on arm processor if the BitConverter.IsLittleEndian is false and how it react
            // not sure do i need this or not
            throw new NotImplementedException();
        }

        if (header.BitDepth > 64)
            throw new BadImageException();
        //TODO: IMPLEMENT: Marge this in same function
        //TODO:INFO: according to wikipidia not found any info related to 24 bit rle image http://www.fileformat.info/format/bmp/egff.htm
        // but according to this looks like their are more than that https://chromium.googlesource.com/chromium/blink.git/+/master/Source/platform/image-decoders/bmp/BMPImageReader.cpp
        // according to following this these does exist but im not gonna bother until i found a image for testing
        if (header.Compression is HeaderCompression.Rle4 or HeaderCompression.Rle8 //or HeaderCompression.24 probly 32 and 16 too
            )
            ProcessRLEImage(result, header, colorTable);
        else
            ProcessNonRLEImage(result, header, colorTable);
        Console.WriteLine($"R: {result[result.Length - 10].Red} G: {result[result.Length - 10].Green} B: {result[result.Length - 10].Blue}");
        return ImageLoader.LoadImage(width, height, ref result);
    }

    private void ProcessRLEImage(Pixels[] result, BMPHeader header, ColorTable? colorTable)
    {
        var currentPos = _fileStream.Position;
        var reader = GetReader(header, colorTable);
        var rleData = new DecodeRLEOfBMP(_fileStream, header);
        Span<byte> data;
        while (true)
        {
            var readingSize = rleData.GetReadSize();
            var needFilled = readingSize < 0;
            var normalizedReadingSize = readingSize < 0 ? readingSize * -1 : readingSize;
            data =// normalizedReadingSize > 1024 ? 
                new byte[normalizedReadingSize]
                //: stackalloc byte[normalizedReadingSize]
                ;
            var read = rleData.Read(data);
            if (read == -1)
                break;
        }

    }

    private void ProcessNonRLEImage(Pixels[] result, BMPHeader header, ColorTable? colorTable)
    {
        var currentPos = _fileStream.Position;
        var reader = GetReader(header, colorTable);
        var rowWithPadding = reader.CalculationOfRowSize();
        var height = header.GetNormalizeHeight();
        Span<byte> pixel = stackalloc byte[header.GetMinimumPixelsSizeInByte()];
        ArraySegment<Pixels> writingSection;
        int writingIndex;

        for (var i = 0; i < height; i++)
        {
            writingIndex = 0;
            _fileStream.Seek(i * rowWithPadding + currentPos, SeekOrigin.Begin);
            writingSection = new ArraySegment<Pixels>(result, GetWritingOffset(i, header), header.Width);
            while (writingIndex < header.Width)
            {
                _fileStream.ReadExactly(pixel);
                reader.Decode(writingSection, pixel, ref writingIndex);
            }
        }
    }

    private static int GetWritingOffset(int i, BMPHeader header) =>
        header.Height < 0
            ? i * header.Width
            : (header.GetNormalizeHeight() - 1 - i) * header.Width;

    private BaseColorReader GetReader(BMPHeader header, ColorTable? colorTable)
    {
        return header.Compression switch
        {
            HeaderCompression.Rgb => new RgbColorReader(header, colorTable),
            HeaderCompression.BitFields or HeaderCompression.AlphaBitFields => new BitFieldColorReader(header),
            HeaderCompression.Rle4 or HeaderCompression.Rle8 => new RleColorReader(header),
            _ => throw new NotImplementedException($"************************************{header.Compression}************************************")
        };
    }
}


