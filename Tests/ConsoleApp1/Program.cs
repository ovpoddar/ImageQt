using ImageQT;

int width = 1000,
    height = 667;
var bytes = File.ReadAllBytes("D:\\ImageQTALLTEST\\MAC\\DONEMAC\\C#MAC\\.DS_Store");

var image = ImageLoader.LoadImage(width, height, ref bytes);


var qt = new ImageQt(image);
await qt.Show();

var qt1 = new ImageQt(image);
qt = new ImageQt(image);
await qt.Show();
await qt1.Show();

for (int i = 0; i < 1; i++)
{
    qt = new ImageQt(image);
    await qt.Show();
}
await qt1.Show();