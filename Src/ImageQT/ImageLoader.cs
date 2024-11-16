using ImageQT.Models.ImagqQT;

namespace ImageQT;
public abstract class ImageLoader
{
    public static Image LoadImage(int width, int height, ref int[] bytes)
    {
        if ((uint)width == 0 || (uint)height == 0)
            throw new Exception("Invalid Size");

        if (bytes.Length != width * height)
            throw new Exception("Insufficent data");
        return new Image<int>(width, height, 1, 1, sizeof(int) * 8, bytes);
    }

    public static Image LoadImage(int width, int height, ref byte[] bytes)
    {

        if ((uint)width == 0 || (uint)height == 0)
            throw new Exception("Invalid Size");

        if (bytes.Length != width * height * 4)
            throw new Exception("Insufficient data");

        return new Image<byte>(width, height, 1, 1, sizeof(int) * 8, bytes);
    }
}
