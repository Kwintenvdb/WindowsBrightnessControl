using System;
using System.Collections.Generic;
using WindowsBrightnessControl.Model;

namespace WindowsBrightnessControl.Service
{
	public interface IMonitorService
	{
		IEnumerable<PhysicalMonitor> GetPhysicalMonitors();
		uint GetMonitorBrightness(IntPtr monitorPtr);
		void SetMonitorBrightness(IntPtr monitorPtr, uint brightness);
	}
}