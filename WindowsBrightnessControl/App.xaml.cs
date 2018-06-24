using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;

namespace TestSetBrightness
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

	[StructLayout(LayoutKind.Sequential)]
	public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
        public static extern void GetMonitorBrightness(IntPtr hMonitor, [Out] out UInt32 pdwMinimumBrightness, [Out] out UInt32 pdwCurrentBrightness, [Out] out UInt32 pdwMaximumBrightness);

        [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
		public static extern void SetMonitorBrightness(IntPtr hMonitor, UInt32 dwNewBrightness);

		[DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
		public static extern void GetNumberOfPhysicalMonitorsFromHMONITOR(IntPtr hMonitor, [Out] out UInt32 pdwNumberOfPhysicalMonitors);

		[DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
		public static extern void GetPhysicalMonitorsFromHMONITOR([In] IntPtr hMonitor, [In] UInt32 dwPhysicalMonitorArraySize, [Out] PHYSICAL_MONITOR[] pPhysicalMonitorArray);

		[DllImport("user32.dll", SetLastError = true)]
        public static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumDelegate lpfnEnum, IntPtr dwData);

		[DllImport("Kernel32.dll", SetLastError = true)]
		public static extern uint GetLastError();

        public delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

			IntPtr monitorPtr = IntPtr.Zero;
			EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, delegate (IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData)
			{
				Console.WriteLine($"Found monitor with ptr: {hMonitor}");
				monitorPtr = hMonitor;
				return true;
			}, IntPtr.Zero);

			GetNumberOfPhysicalMonitorsFromHMONITOR(monitorPtr, out uint nrPhysicalMonitors);
			Console.WriteLine($"Nr physical monitors: {nrPhysicalMonitors}");

			var physicalMonitors = new PHYSICAL_MONITOR[nrPhysicalMonitors];
			GetPhysicalMonitorsFromHMONITOR(monitorPtr, nrPhysicalMonitors, physicalMonitors);

			var hPhysMonitor = physicalMonitors[0].hPhysicalMonitor;
			GetMonitorBrightness(hPhysMonitor, out uint minBrightness, out uint currentBrightness, out uint maxBrightness);
			Console.WriteLine($"Current brightness: {currentBrightness}");

			SetMonitorBrightness(hPhysMonitor, 10);
        }
    }
}
