﻿using ImageQT.Decoder.Helpers;
using ImageQT.Decoder.PNG.Models;
using ImageQT.Decoder.PNG.Models.ColorReader;
using ImageQT.Decoder.PNG.Models.Filters;
using ImageQT.Models.ImagqQT;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.PNG;
internal class PngDecoder : IImageDecoder
{
    private Stream _fileStream;
    private readonly List<PNGChunk> _chunks = new List<PNGChunk>(4);
    private static ReadOnlySpan<byte> HeaderSignature => [137, 80, 78, 71, 13, 10, 26, 10];

    public PngDecoder(Stream stream) =>
        _fileStream = stream;

    public bool CanProcess()
    {
        Span<byte> signature = stackalloc byte[HeaderSignature.Length];
        _fileStream.Read(signature);
        if (!GenericHelper.Equal(signature, HeaderSignature))
            return false;

        while (_fileStream.Position < _fileStream.Length)
        {
            var chunk = new PNGChunk(_fileStream);
            _chunks.Add(chunk);
            if (chunk.Signature == PngChunkType.IEND)
                break;
        }

        if (!_chunks.Any(a => a.Signature == PngChunkType.IDAT)
            || !_chunks.Any(a => a.Signature == PngChunkType.IHDR))
            return false;

        return true;
    }

    public Image Decode()
    {
        Debug.Assert(_fileStream != null);
        var header = _chunks.First(a => a.Signature == PngChunkType.IHDR);
        var headerData = new IHDRData(header);

        var paletteData = new PLTEData?();

        if (headerData.ColorType == ColorType.Palette)
        {
            var palate = _chunks.First(a => a.Signature == PngChunkType.PLTE);
            paletteData = new PLTEData(palate);
        }
        var writingIndex = 0;
        var processRow = 0;
        var result = new byte[headerData.Height * headerData.Width * 4];
        var colorConverter = GetPixelColorDecoder(ref headerData, ref paletteData);

        using var rawstream = GetDecompressedRawStream();
        using var filteredMutableRawStream = new MemoryStream();
        rawstream.CopyTo(filteredMutableRawStream);
        DecodeZLibStream(filteredMutableRawStream, ref headerData, result, ref writingIndex, ref processRow, colorConverter);
        return ImageLoader.LoadImage((int)headerData.Width, (int)headerData.Height, ref result);
    }

    // TODO: change to BGRA 
    // TODO: change into pixel version for converter
    static BaseRGBColorConverter GetPixelColorDecoder(ref IHDRData headerData, ref PLTEData? paletteData) =>
      headerData.ColorType switch
      {
          ColorType.Palette => new PalateColorConverter(paletteData!.Value, headerData),
          ColorType.GreyScale => new GrayScaleColorConverter(headerData),
          ColorType.RGB => new RGBColorConverter(headerData),
          ColorType.GreyScaleAndAlpha => new GreyScaleAndAlphaConverter(headerData),
          ColorType.RGBA => new RGBAColorConverter(headerData),
          _ => throw new NotSupportedException(),
      };

    ZLibStream GetDecompressedRawStream()
    {
        var result = new MemoryStream();
        foreach (var chunk in _chunks.Where(a => a.Signature == PngChunkType.IDAT))
        {
            var data = ArrayPool<byte>.Shared.Rent((int)chunk.Length);
            try
            {
                chunk.GetData(data);
                result.Write(data, 0, (int)chunk.Length);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(data);
            }
        }
        result.Position = 0;
        return new ZLibStream(result, CompressionMode.Decompress, false);
    }

    void DecodeZLibStream(MemoryStream filteredMutableRawStream,
           ref IHDRData headerData,
           byte[] result,
           ref int writingIndex,
           ref int currentRow,
           BaseRGBColorConverter colorConverter)
    {
        filteredMutableRawStream.Position = 0;
        var scanline = headerData.GetScanLinesWidthWithPadding();
        BasePNGFilter? currentFilter = null;
        ArraySegment<byte> writtenSection = new();
        Span<byte> currentByte = headerData.BitDepth < 8
            ? stackalloc byte[1]
            : stackalloc byte[headerData.GetPixelSizeInByte()];
        Span<byte> currentFilterByte = stackalloc byte[1];

        while (filteredMutableRawStream.Position < filteredMutableRawStream.Length)
        {
            if (filteredMutableRawStream.Position % scanline == 0)
            {
                filteredMutableRawStream.ReadExactly(currentFilterByte);
                currentFilter = GetFilter(filteredMutableRawStream, currentFilterByte[0]);
                writtenSection = new ArraySegment<byte>(result, (int)(currentRow++ * headerData.Width * 4), (int)headerData.Width * 4);
                writingIndex = 0;
            }
            else
            {
                filteredMutableRawStream.ReadExactly(currentByte);
                currentByte = currentFilter!.UnApply(currentByte, scanline);
                colorConverter.Write(writtenSection, currentByte, ref writingIndex);
            }
        }
    }

    static BasePNGFilter GetFilter(Stream stream, byte currentByte) =>
      currentByte switch
      {
          0 => new NonFilter(stream),
          1 => new SubFilter(stream),
          2 => new UpFilter(stream),
          3 => new AverageFilter(stream),
          4 => new PaethFilter(stream),
          _ => throw new NotImplementedException()
      };

}
