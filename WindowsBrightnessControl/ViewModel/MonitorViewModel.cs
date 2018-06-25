using System;
using System.Threading.Tasks;
using WindowsBrightnessControl.Model;
using WindowsBrightnessControl.Service;

namespace WindowsBrightnessControl.ViewModel
{
	public class MonitorViewModel : ObservableObject
	{
		private PhysicalMonitor _monitor;
		private IMonitorService _monitorService;

		private uint _cachedBrightness;

		private IntPtr MonitorPtr => _monitor.hPhysicalMonitor;

		public MonitorViewModel(PhysicalMonitor monitor, IMonitorService monitorService)
		{
			_monitor = monitor;
			_monitorService = monitorService;

			_cachedBrightness = _monitorService.GetMonitorBrightness(MonitorPtr);
		}

		public uint Brightness
		{
			get
			{
				return _cachedBrightness;
				//return _monitorService.GetMonitorBrightness(MonitorPtr);
			}
			set
			{
				// Run on a different thread because this is very slow.
				Task.Run(() =>
				{
					_monitorService.SetMonitorBrightness(MonitorPtr, value);
				});
				_cachedBrightness = value;
				RaisePropertyChanged(nameof(Brightness));
			}
		}
	}
}
