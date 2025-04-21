using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux;

// Xcms information structure
[StructLayout(LayoutKind.Sequential)]
public struct XcmsInfo
{
    public IntPtr defaultCCCs;              /* pointer to an array of default XcmsCCC */
    public IntPtr clientCmaps;              /* pointer to linked list of XcmsCmapRec */
    public IntPtr perVisualIntensityMaps;   /* linked list of XcmsIntensityMap */
}
