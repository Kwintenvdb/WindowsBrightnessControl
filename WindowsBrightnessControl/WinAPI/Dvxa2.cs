using System;
using System.Runtime.InteropServices;

namespace WindowsBrightnessControl.WinAPI
{
	public class Dvxa2
	{
		public const string ASSEMBLY_NAME = "Dxva2.dll";

		[DllImport(ASSEMBLY_NAME, ExactSpelling = true, SetLastError = true, PreserveSig = false)]
		public static extern void GetMonitorBrightness(IntPtr hMonitor, [Out] out UInt32 pdwMinimumBrightness, [Out] out UInt32 pdwCurrentBrightness, [Out] out UInt32 pdwMaximumBrightness);

		[DllImport(ASSEMBLY_NAME, ExactSpelling = true, SetLastError = true, PreserveSig = false)]
		public static extern void SetMonitorBrightness(IntPtr hMonitor, UInt32 dwNewBrightness);

		[DllImport(ASSEMBLY_NAME, ExactSpelling = true, SetLastError = true, PreserveSig = false)]
		public static extern void GetNumberOfPhysicalMonitorsFromHMONITOR(IntPtr hMonitor, [Out] out UInt32 pdwNumberOfPhysicalMonitors);

		[DllImport(ASSEMBLY_NAME, ExactSpelling = true, SetLastError = true, PreserveSig = false)]
		public static extern void GetPhysicalMonitorsFromHMONITOR([In] IntPtr hMonitor, [In] UInt32 dwPhysicalMonitorArraySize, [Out] PHYSICAL_MONITOR[] pPhysicalMonitorArray);
	}
}
