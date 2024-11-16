// Ignore Spelling: Mipmaps

namespace ImageQT.Models.ImagqQT;

public abstract record Image(int Width, int Height, int Mipmaps, int Format, short BitCount)
{
    public abstract IntPtr Id { get; }
}
