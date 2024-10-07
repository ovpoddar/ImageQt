using ImageQT.Decoder.Helpers;
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


    public (int count, bool isUndefinedPixel) GetReadSize(int writingIndex, int row)
    {
        ProcessCurrentPosition();
        if (_data[0] == 0)
        {
            switch (_data[1])
            {
                case 0:
                    return (writingIndex == 0 ? 0 : _header.Width - writingIndex, true);

                case 1:
                    return (_header.Width * _header.Height - ((row - 2) * _header.Width) + writingIndex, true);

                case 2:
                    Span<byte> data = stackalloc byte[2];
                    _stream.ReadStreamWithRollBackSeek(data);
                    return (data[1] * _header.Width + data[0], true);

                default:
                    return (MapByteCountToActualMemory(_data[1]), false);

            }
        }
        else
        {
            return (_data[0] * (_header.BitDepth < 16 ? 1 : 3), false);
        }
    }

    public int Read(byte[] buffer, int offset, int count)
    {
        int totalRead;
        if (_data[0] == 0)
        {
            switch (_data[1])
            {
                case 0:
                    _position += count;
                    totalRead = count;
                    break;
                case 2:
                    _position += count;
                    totalRead = count;
                    _stream.Seek(2, SeekOrigin.Current);
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
