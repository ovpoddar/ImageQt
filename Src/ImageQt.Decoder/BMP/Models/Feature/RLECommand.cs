namespace ImageQT.Decoder.BMP.Models.Feature;
internal ref struct RLECommand
{
    public RLECommandType CommandType { get; set; }
    public byte Data1 { get; set; }
    public byte Data2 { get; set; }

}
