using ImageQT.Models.ImagqQT;
using System.Runtime.InteropServices;

namespace ImageQT;
public static class ImageLoader
{
    public static Image LoadImage(int width, int height, ref int[] bytes)
    {
        if ((uint)width == 0 || (uint)height == 0)
            throw new Exception("Invalid Size");

        if (bytes.Length != width * height)
            throw new Exception("Insufficent data");
        var imageData = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);
        return new Image()
        {
            Height = height,
            Width = width,
            Id = imageData,
            Format = 1,
            Mipmaps = 1,
            BitCount = sizeof(int) * 8
        };
    }

    public static Image LoadImage(int width, int height, ref byte[] bytes)
    {

        if ((uint)width == 0 || (uint)height == 0)
            throw new Exception("Invalid Size");

        if (bytes.Length != width * height * 4)
            throw new Exception("Insufficient data");

        var imageData = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);

        return new Image()
        {
            Height = height,
            Width = width,
            Id = imageData,
            Format = 1,
            Mipmaps = 1,
            BitCount = sizeof(int) * 8
        };
    }

    public static Image LoadImage(int width, int height, ref Pixels[] bytes)
    {
        if ((uint)width == 0 || (uint)height == 0)
            throw new Exception("Invalid Size");

        if (bytes.Length != width * height)
            throw new Exception("Insufficent data");

        var imageData = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);

        return new Image()
        {
            Height = height,
            Width = width,
            Id = imageData,
            Format = 1,
            Mipmaps = 1,
            BitCount = sizeof(int) * 8
        };
    }

    public static Image LoadImage(string file)
    {
        throw new NotImplementedException();
    }

    public static void ReleaseImage(Image image)
    {
        if (image.Id != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(image.Id);
        }
    }

}
