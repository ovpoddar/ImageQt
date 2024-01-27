using ImageQt.Exceptions;
using System.Runtime.InteropServices;

namespace ImageQt;
public class Image
{
    private readonly string[] _imageTypes = [".PNG", ".JPGE", ".JPG", ".BMP"];

    internal bool hasPath;
    internal string? path;
    internal uint width;
    internal uint height;
    internal IntPtr imageData;

    void PathImage(FileInfo file)
    {
        if (!file.Exists)
            throw new FileNotFoundException();

        if (!_imageTypes.Contains(file.Extension, StringComparer.OrdinalIgnoreCase))
            throw new FileNotSupportedException();

        hasPath = true;
        path = file.FullName;
    }
    void CustomeImage(int width, int height, ref IntPtr imageData)
    {
        this.width = Convert.ToUInt32(width);
        this.height = Convert.ToUInt32(height);
        this.imageData = imageData;
    }

    public Image(string file) =>
        PathImage(new FileInfo(file));
    public Image(int width, int height, ref byte[] bytes)
    {
        if (width * height * 4 != bytes.Length)
            throw new InsufficientBytesSizeException();
        var imageData = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);
        CustomeImage(width, height, ref imageData);
    }
    public Image(int width, int height, ref int[] bytes)
    {
        if (width * height != bytes.Length)
            throw new InsufficientBytesSizeException();
        var imageData = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);
        CustomeImage(width, height, ref imageData);
    }

}
