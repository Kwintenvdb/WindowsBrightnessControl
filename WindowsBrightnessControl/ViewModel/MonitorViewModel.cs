using System;
using WindowsBrightnessControl.Model;
using WindowsBrightnessControl.Service;

namespace WindowsBrightnessControl.ViewModel
{
	public class MonitorViewModel : ObservableObject
	{
		private PhysicalMonitor _monitor;
		private IMonitorService _monitorService;
		private int _cachedBrightness;

		private IntPtr MonitorPtr => _monitor.hPhysicalMonitor;

		public MonitorViewModel(PhysicalMonitor monitor, IMonitorService monitorService)
		{
			_monitor = monitor;
			_monitorService = monitorService;
			_cachedBrightness = (int)_monitorService.GetMonitorBrightness(MonitorPtr);
		}

		public int Brightness
		{
			get
			{
				return _cachedBrightness;
				//return _monitorService.GetMonitorBrightness(MonitorPtr);
			}
			set
			{
				// Clamp the brightness between 0 and 100.
				// Could replace with WinApi delivered min and max later.
				int newBrightness = Math.Min(100, Math.Max(0, value));
				if (_cachedBrightness != newBrightness)
				{
					_cachedBrightness = newBrightness;
					RaisePropertyChanged(nameof(Brightness));
					_monitorService.SetMonitorBrightness(MonitorPtr, (uint)newBrightness);
				}
			}
		}
	}
}
