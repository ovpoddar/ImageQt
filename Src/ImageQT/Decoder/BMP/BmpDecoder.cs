﻿// Ignore Spelling: Bmp

using ImageQT.Decoder.BMP.Models;
using ImageQT.Decoder.BMP.Models.ColorReader;
using ImageQT.Decoder.BMP.Models.DIbFileHeader;
using ImageQT.Decoder.BMP.Models.Feature;
using ImageQT.Decoder.Helpers;
using ImageQT.Exceptions;
using ImageQT.Models.ImagqQT;
using System.Buffers;
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
            width = header.Width < 0 ? header.Width * -1 : header.Width; // should not be valid.
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

        ProcessImage(result, header, colorTable);
        Console.WriteLine($"R: {result[result.Length - 10].Red} G: {result[result.Length - 10].Green} B: {result[result.Length - 10].Blue}");
        return ImageLoader.LoadImage(width, height, ref result);
    }

    private void ProcessImage(Pixels[] result, BMPHeader header, ColorTable? colorTable)
    {
        var currentPos = _fileStream.Position;
        var reader = GetReader(header, colorTable);
        ArraySegment<Pixels> writingSection;
        var height = header.GetNormalizeHeight();
        var positionTracker = new RLEPositionTracker(header.Height < 0, header.Width, height);

        if (!reader.IsRLE)
        {
            int writingIndex;
            var rowWithPadding = reader.CalculationOfRowSize();
            Span<byte> pixel = stackalloc byte[header.GetMinimumPixelsSizeInByte()];

            for (var i = 0; i < height; i++)
            {
                writingIndex = 0;
                _fileStream.Seek(i * rowWithPadding + currentPos, SeekOrigin.Begin);
                writingSection = new ArraySegment<Pixels>(result, (int)positionTracker.Position, header.Width);
                while (writingIndex < header.Width)
                {
                    _fileStream.ReadExactly(pixel);
                    reader.Decode(writingSection, pixel, ref writingIndex);
                }
                positionTracker.UpdatePositionToNextRowStart();
            }
        }
        else
        {
            var rleReader = (BaseRLEColorReader)reader;
            var rleProcesser = new DecodeBMPRLE(_fileStream);
            Span<byte> readSection = [];

            while (_fileStream.Length >= _fileStream.Position)
            {
                var command = rleProcesser.GetCommand();
                if (command.CommandType == RLECommandType.Default)
                    readSection = rleProcesser.DecodeValue(ref command, header.BitDepth);

                writingSection = rleReader.CalculateWriteSection(result, ref command, (int)positionTracker.Position);
                rleReader.Decode(writingSection, readSection, ref command, ref positionTracker);

                if (command.CommandType == RLECommandType.EOF) break;
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
            HeaderCompression.Rle4 or HeaderCompression.Rle8 => new RleColorReader(_fileStream, header, colorTable),
            _ => throw new NotImplementedException($"************************************{header.Compression}************************************")
        };
    }
}


