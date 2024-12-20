using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ImageQT.Decoder.Helpers;
internal static class GenericHelper
{
#if Windows
    [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
#else
    [DllImport("libc", CallingConvention = CallingConvention.Cdecl)]
#endif
    private static extern int memcmp(IntPtr a1, IntPtr a2, uint count);

    internal static unsafe bool Equal(byte[] data1, byte[] data2)
    {
        if (data1 is null || data2 is null || data1.Length != data2.Length)
            return false;

        fixed (byte* p1 = data1, p2 = data2)
            return memcmp((IntPtr)p1, (IntPtr)p2, (uint)data1.Length * sizeof(byte)) == 0;
    }

    internal static unsafe bool Equal(ReadOnlySpan<byte> data1, ReadOnlySpan<byte> data2)
    {
        if (data1.Length != data2.Length)
            return false;

        fixed (byte* p1 = data1, p2 = data2)
            return memcmp((IntPtr)p1, (IntPtr)p2, (uint)data1.Length * sizeof(byte)) == 0;
    }

    internal static byte[] ToBytes(this ValueType @struct)
    {
        var result = new byte[Marshal.SizeOf(@struct)];
        Unsafe.As<byte, ValueType>(ref result[0]) = @struct;
        return result;
    }

    internal static T ToStruct<T>(this byte[] @bytes) where T : struct =>
        Unsafe.As<byte, T>(ref @bytes[0]);

    internal static T ToStruct<T>(this Span<byte> @bytes) where T : struct =>
        Unsafe.As<byte, T>(ref @bytes[0]);

    internal static T FromStream<T>(this Stream stream,
        long skip = 0,
        SeekOrigin origin = SeekOrigin.Current) where T : struct
    {
        var size = Marshal.SizeOf<T>();
        Span<byte> resultAsByte = stackalloc byte[size];
        stream.Seek(skip, origin);

        var read = stream.Read(resultAsByte);
        Debug.Assert(read == size);

        return resultAsByte.ToStruct<T>();
    }

    internal static int ReadStreamWithRollBackSeek(this Stream stream, Span<byte> bytes)
    {
        if (!stream.CanSeek)
            throw new InvalidOperationException("Stream does not support this operation");
        var position = stream.Position;
        try
        {
            return stream.Read(bytes);
        }
        finally
        {
            stream.Seek(position, SeekOrigin.Begin);
        }
    }
}
