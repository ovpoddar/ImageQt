using ImageQT.Decoder.BMP.Models.Feature;
using ImageQT.Exceptions;
using ImageQT.Models.ImagqQT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
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

    public void ProcessActualCommand(ArraySegment<Pixels> result, ref RLECommand command, Span<byte> readByte, ref RLEPositionTracker positionTracker)
    {
        switch (command.CommandType)
        {
            case RLECommandType.Fill:
                ProcessFill(result, command.Data1, command.Data2);
                positionTracker.SetWithPositionAsRelative(command.Data1);
                break;
            case RLECommandType.Default:
                ProcessDefault(result, command.Data2, readByte);
                positionTracker.SetWithPositionAsRelative(command.Data2);
                break;
            case RLECommandType.EOL:
                result.AsSpan(positionTracker.XWWidth, HeaderDetails.Width - positionTracker.XWWidth)
                    .Fill(DefaultPixel);
                positionTracker.SetWithPositionAsRelative(-1);
                positionTracker.UpdatePositionToNextRowStart();
                break;
            case RLECommandType.EOF:
                if (positionTracker.Position == HeaderDetails.Width)
                    return;

                result.AsSpan()
                    .Fill(DefaultPixel);
                break;
            case RLECommandType.Delta:
                Span<byte> deltaValues = stackalloc byte[2];
                if (_stream.Read(deltaValues) != deltaValues.Length)
                    throw new BadImageException();
                var size = deltaValues[1] * HeaderDetails.Width + deltaValues[0];
                var start = (int)(HeaderDetails.Height < 0
                    ? positionTracker.Position - size
                    : positionTracker.Position);
                result.AsSpan(start, size)
                    .Fill(DefaultPixel);
                positionTracker.SetWithXYAsRelative(deltaValues[0], deltaValues[1]);
                break;
            default:
                throw new BadImageFormatException();
        }
    }

    private int CalculateNormalizeIndex(int currentIndex, int x, int y) =>
        currentIndex + x + (HeaderDetails.Height < 0
            ? (HeaderDetails.GetNormalizeHeight() - 1 - y)
            : y * HeaderDetails.Width * HeaderDetails.Width);

    internal ArraySegment<Pixels> CalculateWriteSection(Pixels[] result, ref RLECommand command, RLEPositionTracker positionTracker) =>
        command.CommandType switch
        {
            RLECommandType.Fill => new ArraySegment<Pixels>(result, (int)positionTracker.Position, command.Data1),
            RLECommandType.Default => new ArraySegment<Pixels>(result, (int)positionTracker.Position, command.Data2),
            RLECommandType.EOF => HeaderDetails.Height > 0
                ? new ArraySegment<Pixels>(result, 0, (int)(positionTracker.Position - 0))
                : new ArraySegment<Pixels>(result, (int)positionTracker.Position, (int)(result.Length - positionTracker.Position)),
            _ => new ArraySegment<Pixels>(result)
        };
}
