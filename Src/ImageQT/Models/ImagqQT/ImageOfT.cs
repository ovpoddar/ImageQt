using System.Runtime.InteropServices;

namespace ImageQT.Models.ImagqQT;

internal sealed record Image<T>(int Width, int Height, int Mipmaps, int Format, short BitCount, T[] ActualBytes) : Image(Width, Height, Mipmaps, Format, BitCount)
{
    internal override GCHandle  Id => GCHandle.Alloc(ActualBytes, GCHandleType.Pinned);
}