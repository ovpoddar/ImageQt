using ImageQT.Models.ImagqQT;
using System.Runtime.InteropServices;

namespace ImageQT;
// TODO: Make Smart bit Count
public static class ImageLoader
{
    public static Image LoadImage(int width, int height, ref int[] bytes)
    {
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
        return default;
    }

    public static void ReleaseImage(Image image)
    {
        if (image.Id != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(image.Id);
        }
    }
}
