using System;
using System.ComponentModel;
using WindowsBrightnessControl.Model;
using WindowsBrightnessControl.Service;

namespace WindowsBrightnessControl.ViewModel
{
	public class MonitorViewModel : INotifyPropertyChanged
	{
		private PhysicalMonitor _monitor;
		private IMonitorService _monitorService;

		public event PropertyChangedEventHandler PropertyChanged;

		private IntPtr MonitorPtr => _monitor.hPhysicalMonitor;

		public MonitorViewModel(PhysicalMonitor monitor, IMonitorService monitorService)
		{
			_monitor = monitor;
			_monitorService = monitorService;
		}

		public uint Brightness
		{
			get
			{
				return _monitorService.GetMonitorBrightness(MonitorPtr);
			}
			set
			{
				_monitorService.SetMonitorBrightness(MonitorPtr, value);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Brightness)));
			}
		}
	}
}
