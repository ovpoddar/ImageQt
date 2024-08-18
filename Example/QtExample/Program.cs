using ImageQT;
using ImageQT.Models.ImagqQT;

//int width = 1000,
//    height = 667;
//var bytes = new Pixels[width * height];
//Array.Fill(bytes, new Pixels(82, 71, 66));
//var image = ImageLoader.LoadImage(width, height, ref bytes);

var folder = @"D:\testP\Bmp";
foreach(var file in Directory.GetFiles(folder))
{
    Console.WriteLine(file);
    var image = ImageLoader.LoadImage(file);
    //var qt = new ImageQt(image);
    //await qt.Show();
}

Console.ReadLine();