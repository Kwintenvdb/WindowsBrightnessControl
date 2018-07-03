using GalaSoft.MvvmLight.CommandWpf;
using System;
using WindowsBrightnessControl.Model;
using WindowsBrightnessControl.Service;

namespace WindowsBrightnessControl.ViewModel
{
	public class SettingsViewModel : ObservableObject
	{
		public RelayCommand SaveSettingsCommand { get; private set; }
		public RelayCommand ResetSettingsCommand { get; private set; }

		public bool RunOnStartUp
		{
			get => _settings.RunOnStartUp;
			set
			{
				if (SetField(ref _settings.RunOnStartUp, value))
				{
					OnLocalSettingsChanged();
				}
			}
		}

		public bool UseHotKeys
		{
			get => _settings.UseHotKeys;
			set
			{
				if (SetField(ref _settings.UseHotKeys, value))
				{
					OnLocalSettingsChanged();
				}
			}
		}

		public bool SnapBrightness
		{
			get => _settings.SnapBrightness;
			set
			{
				if (SetField(ref _settings.SnapBrightness, value))
				{
					OnLocalSettingsChanged();
				}
			}
		}

		public int SnappingInterval
		{
			get => _settings.SnappingInterval;
			set
			{
				if (SetField(ref _settings.SnappingInterval, value))
				{
					OnLocalSettingsChanged();
				}
			}
		}

		public bool UseMouseWheel
		{
			get => _settings.UseMouseWheel;
			set
			{
				if (SetField(ref _settings.UseMouseWheel, value))
				{
					OnLocalSettingsChanged();
				}
			}
		}

		public bool SettingsDirty
		{
			get => !_oldSettings.Equals(_settings);
		}

		public event Action SettingsChanged;

		private Settings _oldSettings;
		private Settings _settings;
		private readonly ISettingsProvider _settingsProvider;
		private readonly IDialogService _dialogService;

		public SettingsViewModel(ISettingsProvider settingsProvider, IDialogService dialogService)
		{
			_settingsProvider = settingsProvider;
			_settingsProvider.SettingsChanged += OnSettingsChanged;
			_dialogService = dialogService;
			GetSettings();

			SaveSettingsCommand = new RelayCommand(SaveSettings, () => SettingsDirty);
			ResetSettingsCommand = new RelayCommand(ResetSettings);
		}

		private void GetSettings()
		{
			_settings = _settingsProvider.GetSettings();
			_oldSettings = _settings;
		}

		public void SaveSettings()
		{
			_settingsProvider.SaveSettings(_settings);
		}

		public void ResetSettings()
		{
			_dialogService.ShowDialog("Use default settings?", "Do you want to revert all settings to default?", (ok) =>
			{
				if (ok)
				{
					_settingsProvider.ResetSettings();
				}
			});
		}

		private void OnLocalSettingsChanged()
		{
			SaveSettingsCommand.RaiseCanExecuteChanged();
		}

		private void OnSettingsChanged()
		{
			GetSettings();
			OnLocalSettingsChanged();
			RaisePropertyChanged(nameof(RunOnStartUp));
			RaisePropertyChanged(nameof(UseHotKeys));
			RaisePropertyChanged(nameof(SnapBrightness));
			RaisePropertyChanged(nameof(SnappingInterval));
			RaisePropertyChanged(nameof(UseMouseWheel));

			SettingsChanged?.Invoke();
		}
	}
}
