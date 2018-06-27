using System.Linq;
using System.Windows.Input;
using WindowsBrightnessControl.HotKey;
using WindowsBrightnessControl.Service;

namespace WindowsBrightnessControl.ViewModel
{
	public class MainViewModel : ObservableObject
	{
		public MonitorViewModel Monitor { get; private set; }
		public SettingsViewModel Settings { get; private set; }

		private readonly IHotKeyProcessor _hotKeyProcessor;
		private readonly ISettingsProvider _settingsProvider;

		public MainViewModel(IMonitorService monitorService, IHotKeyProcessor hotKeyProcessor, ISettingsProvider settingsProvider)
		{
			var monitors = monitorService.GetPhysicalMonitors();
			Monitor = new MonitorViewModel(monitors.First(), monitorService);
			Settings = new SettingsViewModel(settingsProvider);

			_settingsProvider = settingsProvider;
			_hotKeyProcessor = hotKeyProcessor;
			ConfigureHotKeys(hotKeyProcessor);
		}

		private void ConfigureHotKeys(IHotKeyProcessor hotKeyProcessor)
		{
			hotKeyProcessor.AddHotKey(ModifierKeys.Alt, Key.F10, () =>
			{
				Monitor.Brightness += Settings.SnappingInterval;
			});

			hotKeyProcessor.AddHotKey(ModifierKeys.Alt, Key.F9, () =>
			{
				Monitor.Brightness -= Settings.SnappingInterval;
			});
		}

		public SettingsViewModel GetEditingSettingsViewModel()
		{
			return new SettingsViewModel(_settingsProvider);
		}
	}
}
