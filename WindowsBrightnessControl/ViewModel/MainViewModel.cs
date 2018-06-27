using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using WindowsBrightnessControl.HotKey;
using WindowsBrightnessControl.Service;

namespace WindowsBrightnessControl.ViewModel
{
	public class MainViewModel : ObservableObject
	{
		public MonitorViewModel Monitor { get; private set; }
		public SettingsViewModel Settings { get; private set; }

		public ValueViewModel<bool> IsWindowVisible { get; private set; } = new ValueViewModel<bool>(false);

		private readonly IHotKeyProcessor _hotKeyProcessor;
		private readonly ISettingsProvider _settingsProvider;

		private DispatcherTimer _visibilityTimer;

		public MainViewModel(IMonitorService monitorService, IHotKeyProcessor hotKeyProcessor, ISettingsProvider settingsProvider)
		{
			var monitors = monitorService.GetPhysicalMonitors();
			Monitor = new MonitorViewModel(monitors.First(), monitorService);
			Monitor.BrightnessChanged += OnBrightnessChanged;
			Settings = new SettingsViewModel(settingsProvider);

			_settingsProvider = settingsProvider;
			_hotKeyProcessor = hotKeyProcessor;
			ConfigureHotKeys(hotKeyProcessor);
		}

		private void OnBrightnessChanged()
		{
			IsWindowVisible.Value = true;

			if (_visibilityTimer != null)
			{
				_visibilityTimer.Tick -= OnWindowTimerTick;
				_visibilityTimer.Stop();
			}
			_visibilityTimer = new DispatcherTimer();
			_visibilityTimer.Interval = TimeSpan.FromSeconds(3);
			_visibilityTimer.Tick += OnWindowTimerTick;
			_visibilityTimer.Start();
		}

		private void OnWindowTimerTick(object sender, EventArgs e)
		{
			IsWindowVisible.Value = false;

			var timer = (DispatcherTimer)sender;
			timer.Stop();
			timer.Tick -= OnWindowTimerTick;
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
