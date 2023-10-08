var qt = new ImageQt.ImageQt("Open window.");
await qt.LoadImage(@"C:\Users\ayanp\Downloads\peakpxbmp.bmp");
await qt.Run(true);
Console.ReadLine();