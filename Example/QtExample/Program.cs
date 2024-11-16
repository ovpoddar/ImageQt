using ImageQT.Decoder;
using ImageQT.Decoder.Models;

int width = 1000,
    height = 667;
var bytes = new Pixels[width * height];
Array.Fill(bytes, new Pixels(82, 71, 66));
var image = ImageLoader.LoadImage(@"D:\testP\Bmp\badbitssize.bmp");

for (int y = 0; y < 5; y++)
{
    var qt = new ImageQT.ImageQt(image);
    await qt.Show(TimeSpan.FromSeconds(5));
    Console.WriteLine(y);
    Thread.Sleep(500);
}

Console.ReadLine();