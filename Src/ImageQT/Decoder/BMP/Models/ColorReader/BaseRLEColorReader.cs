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

    private int _row = 1;

    public void ProcessActualCommand(ArraySegment<Pixels> result, RLECommand command, Span<byte> readByte, ref RLEPositionTracker positionTracker)
    {
        switch (command.CommandType)
        {
            case RLECommandType.Fill:
                ProcessFill(result, command.Data1, command.Data2);
                positionTracker.SetWithPosition(command.Data1);
                break;
            case RLECommandType.Default:
                ProcessDefault(result, command.Data2, readByte);
                positionTracker.SetWithPosition(command.Data2);
                break;
            case RLECommandType.EOL:
                result.AsSpan(positionTracker.XWWidth, HeaderDetails.Width - positionTracker.XWWidth)
                    .Fill(DefaultPixel);
                positionTracker.SetWithXYValue(0, _row++);
                break;
            case RLECommandType.EOF:
                var endPosition = HeaderDetails.Height > 0
                    ? 0
                    : result.Count;
                var currentPosition = positionTracker.Position;
                FillSection(result, (int)positionTracker.Position, endPosition);
                // not bothering to set it because their will be no more process to begin with.
                break;
            case RLECommandType.Delta:
                Span<byte> deltaValues = stackalloc byte[2];
                if (_stream.Read(deltaValues) != deltaValues.Length)
                    throw new BadImageException();
                // can be simplifyed to do investigate
                var newRelativePosition = CalculateNormalizeIndex((int)positionTracker.Position, deltaValues[0], deltaValues[1]);
                var start = Math.Min(newRelativePosition, (int)positionTracker.Position);
                var size = Math.Max(newRelativePosition, (int)positionTracker.Position) - start;
                result.AsSpan(start, size)
                    .Fill(DefaultPixel);
                positionTracker.SetWithXYValue(positionTracker.XWWidth + deltaValues[0], positionTracker.YHHeight + deltaValues[1]);
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

    internal ArraySegment<Pixels> CalculateWriteSection(Pixels[] result, RLECommand command, RLEPositionTracker positionTracker)
    {
        //command.CommandType is RLECommandType.Default or RLECommandType.Fill
        if (command.CommandType is RLECommandType.Fill)
        {
            return new ArraySegment<Pixels>(result, (int)positionTracker.Position, command.Data1);
        }
        else if (command.CommandType == RLECommandType.Default)
        {
            return new ArraySegment<Pixels>(result, (int)positionTracker.Position, command.Data2);
        }
        return new ArraySegment<Pixels>(result);
    }
}
