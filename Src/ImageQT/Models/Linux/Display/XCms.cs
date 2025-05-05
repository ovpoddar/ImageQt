using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public struct XCms
{
    public nint DefaultCCCs; /* pointer to an array of default XcmsCCC */
    public nint ClientCmaps; /* pointer to linked list of XcmsCmapRec */
    public nint PerVisualIntensityMaps; /* linked list of XcmsIntensityMap */
}