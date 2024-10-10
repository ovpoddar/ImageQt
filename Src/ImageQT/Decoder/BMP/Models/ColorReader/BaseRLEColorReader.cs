using ImageQT.Decoder.BMP.Models.Feature;
using ImageQT.Exceptions;
using ImageQT.Models.ImagqQT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP.Models.ColorReader;
internal abstract class BaseRLEColorReader : BaseColorReader
{
    protected abstract Pixels DefaultPixel { get; }

    public override bool IsRLE => true;

    private readonly Stream _stream;

    public BaseRLEColorReader(Stream stream, BMPHeader header) : base(header) =>
        _stream = stream;

    internal override void Decode(ArraySegment<Pixels> result, Span<byte> pixel, ref int writingIndex)
    {
        throw new NotImplementedException();
    }

    protected abstract void ProcessDefault(ArraySegment<Pixels> result, byte size, Span<byte> readByte);
    protected abstract void ProcessFill(ArraySegment<Pixels> result, byte size, byte colorIndex);

    public void ProcessActualCommand(ArraySegment<Pixels> result, RLECommand command, Span<byte> readByte, ref int row, ref int column)
    {
        switch (command.CommandType)
        {
            case RLECommandType.Fill:
                ProcessFill(result, command.Data1, command.Data2);
                row += command.Data1;
                if (row > HeaderDetails.Width)
                {
                    row = 0;
                    column++;
                }
                break;
            case RLECommandType.Default:
                ProcessDefault(result, command.Data2, readByte);
                row += command.Data2;
                if (row > HeaderDetails.Width)
                {
                    row = 0;
                    column++;
                }
                break;
            case RLECommandType.EOL:
                result.AsSpan(column, HeaderDetails.Width - column).Fill(DefaultPixel);
                row = 0;
                column++;
                break;
            case RLECommandType.EOF:
                var endPosition = HeaderDetails.Height < 0
                    ? (0, 0)
                    : (HeaderDetails.Width, HeaderDetails.Height);
                FillSection(result, row, column, endPosition.Item1, endPosition.Item2);
                break;
            case RLECommandType.Delta:
                Span<byte> deltaValues = stackalloc byte[2];
                if (_stream.Read(deltaValues) != deltaValues.Length)
                    throw new BadImageException();
                var writingIndex = HeaderDetails.Width * row + column;
                var newRelativePosition = CalculateNormalizeIndex(writingIndex, deltaValues[0], deltaValues[1]);
                var start = Math.Min(newRelativePosition, writingIndex);
                var size = Math.Max(newRelativePosition, writingIndex) - start;
                result.AsSpan(start, size)
                    .Fill(DefaultPixel);

                break;
            default:
                throw new BadImageFormatException();
        }
    }

    //TODO:VEFIFY THE IMPLEMENTTION
    private int CalculateNormalizeIndex(int currentIndex, int x, int y) =>
        currentIndex + x + (HeaderDetails.Height < 0
            ? (HeaderDetails.GetNormalizeHeight() - 1 - y)
            : y * HeaderDetails.Width * HeaderDetails.Width);

    private void FillSection(ArraySegment<Pixels> pixels, int startX, int startY, int endX, int endY)
    {
        var startPosition = HeaderDetails.Width * startY + startX;
        var endPosition = HeaderDetails.Width * endY + endX;
        FillSection(pixels, startPosition, endPosition);
    }

    private void FillSection(ArraySegment<Pixels> pixels, int start, int end)
    {
        var startPosition = Math.Min(start, end);
        var size = HeaderDetails.Height < 0
            ? (end - start)
            : (start - end) * -1;

        //pixels.AsSpan(startPosition, size)
        //    .Fill(DefaultPixel);

        for (int i = startPosition; i < size; i++)
        {
            pixels[i] = DefaultPixel;
        }
    }

    internal ArraySegment<Pixels> CalculateWriteSection(Pixels[] result, RLECommand command, int row, int column)
    {
        //command.CommandType is RLECommandType.Default or RLECommandType.Fill
        var currentPosition = HeaderDetails.Width * column + row;
        if (command.CommandType is RLECommandType.Fill)
        {
            return new ArraySegment<Pixels>(result, currentPosition, command.Data1);
        }
        else if (command.CommandType == RLECommandType.Default)
        {
            return new ArraySegment<Pixels>(result, currentPosition, command.Data2);
        }
        return new ArraySegment<Pixels>(result);
    }
}
