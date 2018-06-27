using WindowsBrightnessControl.Model;
using WindowsBrightnessControl.Service;

namespace WindowsBrightnessControl.ViewModel
{
	public class SettingsViewModel : ObservableObject
	{
		public Command SaveSettingsCommand { get; private set; }
		public Command ResetSettingsCommand { get; private set; }

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

		public bool SettingsDirty
		{
			get => !_oldSettings.Equals(_settings);
		}

		private Settings _oldSettings;
		private Settings _settings;
		private readonly ISettingsProvider _settingsProvider;

		public SettingsViewModel(ISettingsProvider settingsProvider)
		{
			_settingsProvider = settingsProvider;
			_settingsProvider.SettingsChanged += OnSettingsChanged;
			GetSettings();

			SaveSettingsCommand = new Command(SaveSettings, () => SettingsDirty);
			ResetSettingsCommand = new Command(ResetSettings);
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
			_settingsProvider.ResetSettings();
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
		}
	}
}
