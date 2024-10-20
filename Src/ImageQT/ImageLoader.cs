using ImageQT.Exceptions;
using ImageQT.Handlers.ImageQT;
using ImageQT.Models.ImagqQT;
using System.Buffers.Text;
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
        return new Image<int>(width, height, 1, 1, sizeof(int) * 8, default)
        {
            ActualBytes = bytes
        };
    }

    public static Image LoadImage(int width, int height, ref byte[] bytes)
    {

        if ((uint)width == 0 || (uint)height == 0)
            throw new Exception("Invalid Size");

        if (bytes.Length != width * height * 4)
            throw new Exception("Insufficient data");

        return new Image<byte>(width, height, 1, 1, sizeof(int) * 8, default)
        {
            ActualBytes = bytes
        };
    }

    public static Image LoadImage(int width, int height, ref Pixels[] bytes)
    {
        if ((uint)width == 0 || (uint)height == 0)
            throw new Exception("Invalid Size");

        if (bytes.Length != width * height)
            throw new Exception("Insufficent data");
        return new Image<Pixels>(width, height, 1, 1, sizeof(int) * 8, default)
        {
            ActualBytes = bytes
        };
    }

    public static Image LoadImage(string file)
    {
        using var stream = GetStream(file);
        var supportedDecoder = SupportedImageDecoder.GetSupportedDecoders(stream);
        foreach (var decoder in supportedDecoder)
        {
            if (!decoder.CanProcess())
                continue;
            return decoder.Decode();
        }
        throw new AskForImageDecoder();
    }


    private static Stream GetStream(string file)
    {
        if (File.Exists(file))
            return File.OpenRead(file);
        return Base64.IsValid(file.AsSpan())
            ? (Stream)new MemoryStream(Convert.FromBase64String(file))
            : throw new NotSupportedException();
    }
    public static void ReleaseImage(Image image)
    {
        if (image.Id != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(image.Id);
        }
    }

}
