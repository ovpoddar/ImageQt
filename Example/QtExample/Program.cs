using ImageQT;

int width = 1000,
    height = 667;
var bytes = File.ReadAllBytes("D:\\ImageQTALLTEST\\MAC\\DONEMAC\\C#MAC\\.DS_Store");

var image = ImageLoader.LoadImage(width, height, ref bytes);
using var qt = new ImageQt(image);
await qt.Show();