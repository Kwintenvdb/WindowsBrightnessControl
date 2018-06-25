using System.Linq;
using System.Windows.Input;
using WindowsBrightnessControl.HotKey;
using WindowsBrightnessControl.Service;

namespace WindowsBrightnessControl.ViewModel
{
	public class MainViewModel : ObservableObject
	{
		public MonitorViewModel Monitor { get; private set; }
		public IHotKeyProcessor HotKeyProcessor { get; private set; }

		public MainViewModel(IMonitorService monitorService, IHotKeyProcessor hotKeyProcessor)
		{
			var monitors = monitorService.GetPhysicalMonitors();
			Monitor = new MonitorViewModel(monitors.First(), monitorService);

			HotKeyProcessor = hotKeyProcessor;
			ConfigureHotKeys(hotKeyProcessor);
		}

		private void ConfigureHotKeys(IHotKeyProcessor hotKeyProcessor)
		{
			// Retrieve from settings later.
			const int brightnessInterval = 10;

			hotKeyProcessor.AddHotKey(ModifierKeys.Alt, Key.F10, () =>
			{
				Monitor.Brightness += brightnessInterval;
			});

			hotKeyProcessor.AddHotKey(ModifierKeys.Alt, Key.F9, () =>
			{
				Monitor.Brightness -= brightnessInterval;
			});
		}
	}
}
