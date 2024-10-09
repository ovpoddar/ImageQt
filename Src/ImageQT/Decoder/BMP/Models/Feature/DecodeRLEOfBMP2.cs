using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP.Models.Feature;
internal class DecodeRLEOfBMP2
{
    private readonly Stream _stream;
    private readonly byte[] _data = new byte[2];

    public DecodeRLEOfBMP2(Stream stream) =>
        _stream = stream;

    public RLECommand GetCommand()
    {
        var read = _stream.Read(_data, 0, _data.Length);
        if (_stream.Position > _stream.Length || read != _data.Length)
            throw new BadImageFormatException();
        var readCommand = BinaryPrimitives.ReadInt16BigEndian(_data);
        return new RLECommand
        {
            Data1 = _data[0],
            Data2 = _data[1],
            CommandType = readCommand switch
            {
                < 3 => (RLECommandType)readCommand,
                < 256 => RLECommandType.Default,
                _ => RLECommandType.Fill,
            }
        };
    }
}
