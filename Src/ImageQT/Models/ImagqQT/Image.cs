﻿// Ignore Spelling: Mipmaps

using System.Runtime.InteropServices;

namespace ImageQT.Models.ImagqQT;

public abstract record Image(int Width, int Height, int Mipmaps, int Format, short BitCount)
{
    internal abstract GCHandle Id { get; }
    
    public void Release() =>
        Id.Free();
}
