using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP.Models.Feature;
internal class DecodeRLEOfBMP : Stream
{
    private readonly Stream _stream;
    private readonly BMPHeader _header;

    private bool _isProcessed = true;
    private byte[] _data = new byte[2];


    public override bool CanRead => true;

    public override bool CanSeek => false;

    public override bool CanWrite => false;

    public override long Length { get; }

    public override long Position { get; set; }

    public DecodeRLEOfBMP(Stream stream, BMPHeader header)
    {
        _stream = stream;
        _header = header;
        Length = header.Width * header.Height * (_header.BitDepth < 16 ? 1 : 3);
        Position = 0;
        ProcessCurrentPosition();
    }

    public override void Flush()
    {
        throw new NotImplementedException();
    }


    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new NotImplementedException();
    }

    public override void SetLength(long value)
    {
        throw new NotImplementedException();
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        throw new NotImplementedException();
    }

    private bool ProcessCurrentPosition()
    {
        if (!_isProcessed)
            return true;

        var read = _stream.Read(_data);
        if (read != 2)
            return false;

        _isProcessed = false;
        return true;
    }

    private int MapByteCountToActualMemory(uint value) =>
        _header.BitDepth switch
        {
            4 => (int)((value + 1) * .5),
            8 => (int)(value),
            24 => (int)(value * 3),
            _ => throw new NotSupportedException()
        };

    public int GetReadSize()
    {
        ProcessCurrentPosition();

        if (_data[0] == 0)
        {
            switch (_data[1])
            {
                case 0:
                    return (int)(_header.Width - Position % _header.Width) * -1;
                case 1:
                    return (int)(_header.Width * _header.Height - Position) * -1;
                case 2:
                    var num = _stream.ReadByte();
                    var num2 = _stream.ReadByte();
                    _stream.Seek(-2L, SeekOrigin.Current);
                    return (num2 * _header.Width + num) * -1;
                default:
                    return MapByteCountToActualMemory(_data[1]);
            }
        }
        else
        {
            return _data[0] * (_header.BitDepth < 16 ? 1 : 3);
        }
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        int totalRead;
        if (_data[0] == 0)
        {
            switch (_data[1])
            {
                case 0:
                case 2:
                    Position += count;
                    totalRead = count;
                    break;
                case 1:
                    Position += count;
                    totalRead = -1;
                    break;
                default:
                    totalRead = _stream.Read(buffer, offset, count);
                    Position += _data[1];
                    var padding = count & 1;
                    _stream.Seek(padding, SeekOrigin.Current);
                    break;
            }
        }
        else
        {
            if (_header.BitDepth < 16)
            {
                var writingSection = new Span<byte>(buffer, offset, count);
                writingSection.Fill(_data[1]);
                Position += writingSection.Length;
                totalRead = writingSection.Length;
            }
            else
            {
                Span<byte> otherPixels = stackalloc byte[2];
                _stream.Read(otherPixels);
                while (Position < (Position + count))
                {
                    buffer[offset + Position++] = _data[1];
                    buffer[offset + Position++] = otherPixels[0];
                    buffer[offset + Position++] = otherPixels[1];
                }
                totalRead = count;
            }
        }
        return totalRead;
    }
}
