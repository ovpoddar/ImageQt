var qt = new ImageQt.ImageQt("Open window.");

//await qt.LoadImage(@"C:\Users\ayanp\Downloads\peakpxbmp.bmp");

var bytes = new int[50 * 50];

for (var i = 0; i < 50 * 50; i++)
{
    int value = 0xFF00FF;
    bytes[i] = value;

}

qt.GenerateTheBitMap(50, 50, ref bytes);
await qt.Run(true);
Console.ReadLine();