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
			set => SetLocalSetting(ref _settings.RunOnStartUp, value);
		}

		public bool UseHotKeys
		{
			get => _settings.UseHotKeys;
			set => SetLocalSetting(ref _settings.UseHotKeys, value);
		}

		public bool SnapBrightness
		{
			get => _settings.SnapBrightness;
			set => SetLocalSetting(ref _settings.SnapBrightness, value);
		}

		public int SnappingInterval
		{
			get => _settings.SnappingInterval;
			set => SetLocalSetting(ref _settings.SnappingInterval, value);
		}

		public bool UseMouseWheel
		{
			get => _settings.UseMouseWheel;
			set => SetLocalSetting(ref _settings.UseMouseWheel, value);
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

		private void SetLocalSetting<T>(ref T settingField, T value)
		{
			if (SetField(ref settingField, value))
			{
				OnLocalSettingsChanged();
			}
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
