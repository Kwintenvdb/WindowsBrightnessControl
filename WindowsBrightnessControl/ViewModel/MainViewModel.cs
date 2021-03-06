﻿using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Linq;
using System.Windows;
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

		public RelayCommand<MouseWheelEventArgs> MouseWheelCommand { get; private set; }
		public RelayCommand ShowWindowCommand { get; private set; }
		public RelayCommand ExitApplicationCommand { get; private set; }

		public RelayCommand IncreaseBrightnessCommand { get; private set; }
		public RelayCommand DecreaseBrightnessCommand { get; private set; }

		private readonly HotKeyManagerViewModel _hotKeyManager;
		private readonly ISettingsProvider _settingsProvider;
		private readonly IStartupService _startupService;
		private readonly IDialogService _dialogService;

		private DispatcherTimer _visibilityTimer;

		public MainViewModel(IMonitorService monitorService, IHotKeyService hotKeyService, ISettingsProvider settingsProvider,
			IStartupService startupService, IDialogService dialogService)
		{
			var monitors = monitorService.GetPhysicalMonitors();
			Monitor = new MonitorViewModel(monitors.First(), monitorService);
			Monitor.BrightnessChanged += OnBrightnessChanged;

			Settings = new SettingsViewModel(settingsProvider, dialogService);
			Settings.SettingsChanged += OnSettingsChanged;

			MouseWheelCommand = new RelayCommand<MouseWheelEventArgs>(OnMouseWheelScroll);
			ShowWindowCommand = new RelayCommand(ShowWindow);
			ExitApplicationCommand = new RelayCommand(ExitApplication);

			IncreaseBrightnessCommand = new RelayCommand(IncreaseBrightness);
			DecreaseBrightnessCommand = new RelayCommand(DecreaseBrightness);

			_settingsProvider = settingsProvider;
			_startupService = startupService;
			_startupService.RunAppOnStartup(Settings.RunOnStartUp);
			_hotKeyManager = new HotKeyManagerViewModel(hotKeyService, this);
			_dialogService = dialogService;
		}

		private void OnBrightnessChanged()
		{
			ShowWindow();
		}

		private void OnSettingsChanged()
		{
			_startupService.RunAppOnStartup(Settings.RunOnStartUp);
		}

		// The MouseWheelEventArgs break MVVM pattern but requires too much
		// boilerplate implementation effort to get rid of.
		private void OnMouseWheelScroll(MouseWheelEventArgs args)
		{
			if (!Settings.UseMouseWheel) return;

			int direction = Math.Sign(args.Delta);
			if (direction > 0)
			{
				// This sort of thing is repeated quite often... Refactor?
				//Monitor.Brightness += Settings.SnappingInterval;
				IncreaseBrightness();
			}
			else
			{
				//Monitor.Brightness -= Settings.SnappingInterval;
				DecreaseBrightness();
			}
		}

		public void IncreaseBrightness()
		{
			Monitor.Brightness += Settings.SnappingInterval;
		}

		public void DecreaseBrightness()
		{
			Monitor.Brightness -= Settings.SnappingInterval;
		}

		private void ShowWindow()
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
			return new SettingsViewModel(_settingsProvider, _dialogService);
		}

		public void ExitApplication()
		{
			Application.Current.Shutdown();
		}
	}
}
