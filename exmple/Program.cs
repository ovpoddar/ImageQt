
var qt = new ImageQt.ImageQt("Open window.");

/* generate bytes
 * var bim = new Bitmap(@"side-view-smiley-woman-holding-hen.jpg");
 * var fs = new MemoryStream();
 * fs.Seek(0, SeekOrigin.Begin);
 * for (var y = 0; y < bim.Height; y++)
 * {
 *      for (var x = 0; x < bim.Width; x++)
 *      {
 *          var color = bim.GetPixel(x, y).ToArgb();
 *          fs.Write(BitConverter.GetBytes(color));
 *      }
 *  }
 *  fs.Close();
 *  var by = fs.ToArray(); 
 */


int width = 1000,
    height = 667;
var bytes = File.ReadAllBytes("bytes");

qt.GenerateTheBitMap(width, height, ref bytes);

await qt.Run(true);
Console.ReadLine();