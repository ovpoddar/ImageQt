namespace ImageQT.Decoder.BMP.Models.Feature;
internal enum RLECommandType
{
    EOL = 0,
    EOF = 1,
    Delta = 2,
    Default,
    Fill
}
