using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Linux;
[StructLayout(LayoutKind.Sequential)]
public struct XCms
{
    public IntPtr DefaultCCCs;/* pointer to an array of default XcmsCCC */
    public IntPtr ClientCmaps;/* pointer to linked list of XcmsCmapRec */
    public IntPtr PerVisualIntensityMaps;/* linked list of XcmsIntensityMap */
}
