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
			var displayMonitorPtrs = new List<IntPtr>();
			User32.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, (IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData) =>
			{
				displayMonitorPtrs.Add(hMonitor);
				return true;
			}, IntPtr.Zero);

			var physicalMonitors = new List<PhysicalMonitor>();
			foreach (var displayMonitorPtr in displayMonitorPtrs)
			{
				Dvxa2.GetNumberOfPhysicalMonitorsFromHMONITOR(displayMonitorPtr, out uint nrPhysicalMonitors);
				var physMonitors = new PHYSICAL_MONITOR[nrPhysicalMonitors];
				Dvxa2.GetPhysicalMonitorsFromHMONITOR(displayMonitorPtr, nrPhysicalMonitors, physMonitors);

				// Need to decide if this conversion is really necessary...
				physicalMonitors.AddRange(physMonitors.Select(x => new PhysicalMonitor()));
			}
			return physicalMonitors;
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
