using ImageQT.Decoder.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.Formats.PNG.Models;
internal struct PngChunk
{
    private readonly Stream _stream;

    public uint Length { get; set; }
    public long Signature { get; set; }
    public long Data { get; set; }
    public long CRC { get; set; }

    public PngChunk(Stream stream)
    {
        _stream = stream;

        Span<byte> result = stackalloc byte[4];
        stream.Read(result);
        MemoryExtensions.Reverse(result);
        this.Length = result.ToStruct<uint>();

        this.Signature = stream.Position;
        _stream.Seek(4, SeekOrigin.Current);

        this.Data = stream.Position;
        _stream.Seek(this.Length, SeekOrigin.Current);

        this.CRC = stream.Position;
        _stream.Seek(4, SeekOrigin.Current);
    }

    public ReadOnlySpan<char> GetSignature()
    {
        var oldPosation = _stream.Position;
        Span<byte> result = stackalloc byte[4];
        _stream.Seek(this.Signature, SeekOrigin.Begin);
        _stream.Read(result);
        _stream.Seek(oldPosation, SeekOrigin.Begin);
        return Encoding.ASCII.GetString(result).AsSpan();
    }

    public byte[] GetData()
    {
        var oldPosation = _stream.Position;
        var result = new byte[this.Length];
        _stream.Seek(Data, SeekOrigin.Begin);
        _stream.Read(result, 0, result.Length);
        _stream.Seek(oldPosation, SeekOrigin.Begin);
        return result;
    }


    public byte[] GetCRC()
    {
        var oldPosation = _stream.Position;
        Span<byte> result = stackalloc byte[4];
        _stream.Seek(Data, SeekOrigin.Begin);
        _stream.Read(result);
        _stream.Seek(oldPosation, SeekOrigin.Begin);
        return result.ToArray();
    }
}