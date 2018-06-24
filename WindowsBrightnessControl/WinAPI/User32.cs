using System;
using System.Runtime.InteropServices;

namespace WindowsBrightnessControl.WinAPI
{
	public delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);

	public class User32
	{
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumDelegate lpfnEnum, IntPtr dwData);
	}
}
