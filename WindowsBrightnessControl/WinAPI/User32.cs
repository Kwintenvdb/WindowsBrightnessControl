using System;
using System.Runtime.InteropServices;

namespace WindowsBrightnessControl.WinAPI
{
	public delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);

	public class User32
	{
		public const string ASSEMBLY_NAME = "user32.dll";

		[DllImport(ASSEMBLY_NAME, SetLastError = true)]
		public static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumDelegate lpfnEnum, IntPtr dwData);

		[DllImport(ASSEMBLY_NAME, SetLastError = true)]
		public static extern bool RegisterHotKey(IntPtr hWnd, Int32 id, UInt32 fsModifiers, UInt32 vk);
	}
}
