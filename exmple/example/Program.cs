using System.Drawing;
using System.IO;
using System.Text;

var qt = new ImageQt.ImageQt("Open window.");

var bim = new Bitmap(@"side-view-smiley-woman-holding-hen.jpg");
var fs = new MemoryStream();
fs.Seek(0, SeekOrigin.Begin);
for (var y = 0; y < bim.Height; y++)
{
    for (var x = 0; x < bim.Width; x++)
    {
        var color = bim.GetPixel(x, y).ToArgb();
        fs.Write(BitConverter.GetBytes(color));
    }
}
fs.Close();
var by = fs.ToArray();

qt.GenerateTheBitMap(bim.Width, bim.Height, ref by);
await qt.Run(true);
Console.ReadLine();