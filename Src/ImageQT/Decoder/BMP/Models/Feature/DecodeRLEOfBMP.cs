using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP.Models.Feature;
internal class DecodeRLEOfBMP
{
    private readonly Stream _stream;
    private readonly BMPHeader _header;
    private readonly byte[] _data = new byte[2];

    private bool _isProcessed = true;
    private long _position = 0;

    public DecodeRLEOfBMP(Stream stream, BMPHeader header)
    {
        _stream = stream;
        _header = header;
        ProcessCurrentPosition();
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


    public int GetReadSize(int writingIndex)
    {
        if (!ProcessCurrentPosition())
            return 0;
        int result;
        if (_data[0] == 0)
        {
            switch (_data[1])
            {
                case 0:
                    result = (int)(_header.Width - writingIndex % _header.Width);
                    break;
                case 1:
                    result = (int)(_header.Width * _header.Height - writingIndex);
                    break;
                case 2:
                    var num = _stream.ReadByte();
                    var num2 = _stream.ReadByte();
                    result = (num2 * _header.Width + num);
                    break;
                default:
                    result = MapByteCountToActualMemory(_data[1]);
                    break;
            }
        }
        else
        {
            result = _data[0] * (_header.BitDepth < 16 ? 1 : 3);
        }
        return result;
    }

    public int Read(byte[] buffer, int offset, int count)
    {
        int totalRead;
        if (_data[0] == 0)
        {
            switch (_data[1])
            {
                case 0:
                case 2:
                    _position += count;
                    totalRead = count;
                    break;
                case 1:
                    _position += count;
                    totalRead = -1;
                    break;
                default:
                    totalRead = _stream.Read(buffer, offset, count);
                    _position += _data[1];
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
                _position += writingSection.Length;
                totalRead = writingSection.Length;
            }
            else
            {
                Span<byte> otherPixels = stackalloc byte[2];
                _stream.Read(otherPixels);
                while (_position < (_position + count))
                {
                    buffer[offset + _position++] = _data[1];
                    buffer[offset + _position++] = otherPixels[0];
                    buffer[offset + _position++] = otherPixels[1];
                }
                totalRead = count;
            }
        }
        _isProcessed = true;
        return totalRead;
    }
}
