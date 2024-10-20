// Ignore Spelling: Mipmaps

using System.Runtime.InteropServices;

namespace ImageQT.Models.ImagqQT;

internal sealed record Image<T>(int Width, int Height, int Mipmaps, int Format, short BitCount, T[] ActualBytes) : Image(Width, Height, Mipmaps, Format, BitCount)
{
    public override IntPtr Id => Marshal.UnsafeAddrOfPinnedArrayElement(ActualBytes, 0);
}

public abstract record Image(int Width, int Height, int Mipmaps, int Format, short BitCount)
{
    public abstract IntPtr Id { get; }
}
