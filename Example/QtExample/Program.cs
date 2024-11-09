using ImageQT;
using ImageQT.Models.ImagqQT;

int width = 1000,
    height = 667;
var bytes = new Pixels[width * height];
Array.Fill(bytes, new Pixels(82, 71, 66));
var image = ImageLoader.LoadImage(width, height, ref bytes);

for (int y = 0; y < 5; y++)
{
    var qt = new ImageQt(image);
    await qt.Show(TimeSpan.FromSeconds(5));
    Console.WriteLine(y);
    Thread.Sleep(500);
}

Console.ReadLine();