using System;
using System.Runtime.InteropServices;

namespace WindowsBrightnessControl.WinAPI
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
	public struct PHYSICAL_MONITOR
	{
		public IntPtr hPhysicalMonitor;

		// A physical monitor description is always an array of 128 characters.  Some
		// of the characters may not be used.
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string szPhysicalMonitorDescription;
	}
}
