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

    public void ProcessActualCommand(ArraySegment<Pixels> result, Span<byte> processByte, ref RLECommand command, ref RLEPositionTracker positionTracker)
    {
        switch (command.CommandType)
        {
            case RLECommandType.Fill:
                ProcessFill(result, command.Data1, command.Data2);
                positionTracker.SetWithPositionAsAbsolute(positionTracker.Position + command.Data1);
                break;
            case RLECommandType.Default:
                ProcessDefault(result, command.Data2, processByte);
                positionTracker.SetWithPositionAsAbsolute(positionTracker.Position + command.Data2);
                break;
            case RLECommandType.EOL:
                var currentX = positionTracker.GetCurrentX();
                result.AsSpan(currentX, HeaderDetails.Width - currentX)
                    .Fill(DefaultPixel);
                positionTracker.SetWithPositionAsAbsolute(positionTracker.Position - 1);
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

                var deltaX = deltaValues[0];
                var deltaY = deltaValues[1];
                var size = deltaY * HeaderDetails.Width + deltaX;

                result.AsSpan(
                    (int)(HeaderDetails.Height < 0 ? positionTracker.Position - size : positionTracker.Position),
                    size)
                    .Fill(DefaultPixel);
                positionTracker.SetWithXYAsRelative(deltaX, deltaY);
                break;
            default:
                throw new BadImageFormatException();
        }
    }

    internal ArraySegment<Pixels> CalculateWriteSection(Pixels[] result, ref RLECommand command, int position) =>
        command.CommandType switch
        {
            RLECommandType.Fill => new ArraySegment<Pixels>(result, position, command.Data1),
            RLECommandType.Default => new ArraySegment<Pixels>(result, position, command.Data2),
            RLECommandType.EOF => HeaderDetails.Height > 0
                ? new ArraySegment<Pixels>(result, 0, position - 0)
                : new ArraySegment<Pixels>(result, position, result.Length - position),
            _ => new ArraySegment<Pixels>(result)
        };
}
