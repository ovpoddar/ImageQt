using ImageQT;

int width = 1000,
    height = 667;
// red
var bytes = new byte[width * height * 4];
for (var y = 0; y < bytes.Length; y += 4)
{
    bytes[y] = 0;
    bytes[y + 1] = 0;
    bytes[y + 2] = 255;
    bytes[y + 3] = 0;
}
var image = ImageLoader.LoadImage(width, height, ref bytes);
var qt = new ImageQt(image);
await qt.Show();

// green
for (var y = 0; y < bytes.Length; y += 4)
{
    bytes[y] = 0;
    bytes[y + 1] = 255;
    bytes[y + 2] = 0;
    bytes[y + 3] = 0;
}
image = ImageLoader.LoadImage(width, height, ref bytes);
qt = new ImageQt(image);
await qt.Show();


// blue
for (var y = 0; y < bytes.Length; y += 4)
{
    bytes[y] = 255;
    bytes[y + 1] = 0;
    bytes[y + 2] = 0;
    bytes[y + 3] = 0;
}
image = ImageLoader.LoadImage(width, height, ref bytes);
qt = new ImageQt(image);
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
// should throw Exception;
await qt1.Show();