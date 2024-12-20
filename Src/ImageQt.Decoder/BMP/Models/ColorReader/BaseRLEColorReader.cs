﻿using ImageQT.Decoder.BMP.Models.Feature;
using ImageQT.Decoder.Exceptions;
using ImageQT.Decoder.Models;

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
    protected abstract void ProcessFill(ArraySegment<Pixels> result, byte colorIndex);

    public void Decode(ArraySegment<Pixels> result, Span<byte> processByte, ref RLECommand command, ref RLEPositionTracker positionTracker)
    {
        switch (command.CommandType)
        {
            case RLECommandType.Fill:
                ProcessFill(result, command.Data2);
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
                positionTracker.UpdatePositionToNextRowStart(-1);
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

                positionTracker.SetWithXYAsRelative(deltaValues[0], deltaValues[1]);
                break;
            default:
                throw new BadImageException();
        }
    }

    internal (int position, int count) CalculateWriteSection(int totalSize, int position, ref RLECommand command)
    {
        var result = GetWriteSection(totalSize, position, ref command);
        if (result.position + result.count > totalSize)
            throw new BadImageException();
        return result;
    }

    private (int position, int count) GetWriteSection(int totalSize, int position, ref RLECommand command) =>
        command.CommandType switch
        {
            RLECommandType.Fill => (position, command.Data1),
            RLECommandType.Default => (position, MapByteCountToActualMemory(command.Data2, HeaderDetails.BitDepth)),
            RLECommandType.EOF => HeaderDetails.Height > 0
                ? (0, Math.Max(position - 0, 0))
                : (position, totalSize - position),
            _ => (0, totalSize)
        };

    private static int MapByteCountToActualMemory(uint value, int bitDepth) =>
       bitDepth switch
       {
           4 or 8 => (int)(value),
           24 => (int)(value * 3),
           _ => throw new BadImageException()
       };

}
