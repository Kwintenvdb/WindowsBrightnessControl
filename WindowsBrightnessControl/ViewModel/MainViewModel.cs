using System;
using System.Linq;
using System.Windows;
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

		public Command ExitApplicationCommand { get; private set; }

		private readonly HotKeyManagerViewModel _hotKeyManager;
		//private readonly IHotKeyProcessor _hotKeyProcessor;
		private readonly ISettingsProvider _settingsProvider;

		private DispatcherTimer _visibilityTimer;

		public MainViewModel(IMonitorService monitorService, IHotKeyProcessor hotKeyProcessor, ISettingsProvider settingsProvider)
		{
			var monitors = monitorService.GetPhysicalMonitors();
			Monitor = new MonitorViewModel(monitors.First(), monitorService);
			Monitor.BrightnessChanged += OnBrightnessChanged;
			Settings = new SettingsViewModel(settingsProvider);

			ExitApplicationCommand = new Command(ExitApplication);

			_settingsProvider = settingsProvider;
			_hotKeyManager = new HotKeyManagerViewModel(hotKeyProcessor, this);
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

		public SettingsViewModel GetEditingSettingsViewModel()
		{
			return new SettingsViewModel(_settingsProvider);
		}

		public void ExitApplication()
		{
			Application.Current.Shutdown();
		}
	}
}
