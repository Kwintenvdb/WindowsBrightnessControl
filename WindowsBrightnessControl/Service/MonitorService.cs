using System;
using System.Collections.Generic;
using System.Linq;
using WindowsBrightnessControl.Model;
using WindowsBrightnessControl.WinAPI;

namespace WindowsBrightnessControl.Service
{
	public class MonitorService : IMonitorService
	{
		public IEnumerable<PhysicalMonitor> GetPhysicalMonitors()
		{
			List<IntPtr> displayMonitorPtrs = GetDisplayMonitorPtrs();
			return displayMonitorPtrs.SelectMany(ptr =>
			{
				PHYSICAL_MONITOR[] physMonitors = GetPhysicalMonitors(ptr);
				return physMonitors.Select(x => new PhysicalMonitor()
				{
					hPhysicalMonitor = x.hPhysicalMonitor,
					szPhysicalMonitorDescription = x.szPhysicalMonitorDescription
				});
			});
		}

		private static List<IntPtr> GetDisplayMonitorPtrs()
		{
			var displayMonitorPtrs = new List<IntPtr>();
			User32.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, (IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData) =>
			{
				displayMonitorPtrs.Add(hMonitor);
				return true;
			}, IntPtr.Zero);
			return displayMonitorPtrs;
		}

		private static PHYSICAL_MONITOR[] GetPhysicalMonitors(IntPtr displayMonitorPtr)
		{
			Dvxa2.GetNumberOfPhysicalMonitorsFromHMONITOR(displayMonitorPtr, out uint nrPhysicalMonitors);
			var physMonitors = new PHYSICAL_MONITOR[nrPhysicalMonitors];
			Dvxa2.GetPhysicalMonitorsFromHMONITOR(displayMonitorPtr, nrPhysicalMonitors, physMonitors);
			return physMonitors;
		}

		public uint GetMonitorBrightness(IntPtr monitorPtr)
		{
			Dvxa2.GetMonitorBrightness(monitorPtr, out uint minBrightness, out uint currentBrightness, out uint maxBrightness);
			return currentBrightness;
		}

		public void SetMonitorBrightness(IntPtr monitorPtr, uint brightness)
		{
			Dvxa2.SetMonitorBrightness(monitorPtr, brightness);
		}
	}
}
