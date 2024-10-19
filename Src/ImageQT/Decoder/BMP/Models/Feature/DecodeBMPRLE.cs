using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP.Models.Feature;
internal class DecodeBMPRLE
{
    private readonly Stream _stream;
    private readonly byte[] _data = new byte[2];

    public DecodeBMPRLE(Stream stream) =>
        _stream = stream;

    public RLECommand GetCommand()
    {
        if (_stream.Position > _stream.Length || _stream.Read(_data, 0, _data.Length) != _data.Length)
            throw new BadImageFormatException();

        var readCommand = BinaryPrimitives.ReadInt16BigEndian(_data);
        return new RLECommand
        {
            Data1 = _data[0],
            Data2 = _data[1],
            CommandType = readCommand switch
            {
                > 0 and < 3 => (RLECommandType)readCommand,
                < 256 => RLECommandType.Default,
                _ => RLECommandType.Fill,
            }
        };
    }

    internal byte[] DecodeValue(ref RLECommand command, int bitDepth)
    {
        byte[] result = [];
        if (command.CommandType == RLECommandType.Default)
        {
            var unPaddedReadingByte = MapTotalRead(command.Data2, bitDepth);
            result = new byte[unPaddedReadingByte];
            _stream.Read(result, 0, result.Length);

            var padding = unPaddedReadingByte & 1;
            _stream.Seek(padding, SeekOrigin.Current);
        }

        return result;
    }

    private static int MapTotalRead(int reading, int bitDepth)
    {
        var pixelsPerByte = 8 / bitDepth;
        var bytesPerPixel = bitDepth / 8;
        return (bitDepth < 16) ? ((reading + pixelsPerByte - 1) / pixelsPerByte) : (reading * bytesPerPixel);
    }
}
