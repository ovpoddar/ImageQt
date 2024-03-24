using ImageQT;

int width = 1000,
    height = 667;
var bytes = File.ReadAllBytes("bytes");

var image = ImageLoader.LoadImage(width, height, ref bytes);
using var qt = new ImageQt(image);
await qt.Show();