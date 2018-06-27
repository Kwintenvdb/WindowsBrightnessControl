using WindowsBrightnessControl.Model;
using WindowsBrightnessControl.Service;

namespace WindowsBrightnessControl.ViewModel
{
	public class SettingsViewModel : ObservableObject
	{
		public Command SaveSettingsCommand { get; private set; }

		public bool RunOnStartUp
		{
			get => _settings.RunOnStartUp;
			set => SetField(ref _settings.RunOnStartUp, value);
		}

		public bool UseHotKeys
		{
			get => _settings.UseHotKeys;
			set => SetField(ref _settings.UseHotKeys, value);
		}

		public bool SnapBrightness
		{
			get => _settings.SnapBrightness;
			set => SetField(ref _settings.SnapBrightness, value);
		}

		public int SnappingInterval
		{
			get => _settings.SnappingInterval;
			set => SetField(ref _settings.SnappingInterval, value);
		}

		private Settings _settings;
		private readonly ISettingsProvider _settingsProvider;

		public SettingsViewModel(ISettingsProvider settingsProvider)
		{
			_settingsProvider = settingsProvider;
			_settingsProvider.SettingsChanged += OnSettingsChanged;
			_settings = settingsProvider.GetSettings();
			// Add settings dirty check func for CanExecute.
			SaveSettingsCommand = new Command(SaveSettings);
		}

		public void SaveSettings()
		{
			_settingsProvider.SaveSettings(_settings);
		}

		private void OnSettingsChanged()
		{
			_settings = _settingsProvider.GetSettings();
			RaisePropertyChanged(nameof(RunOnStartUp));
			RaisePropertyChanged(nameof(UseHotKeys));
			RaisePropertyChanged(nameof(SnapBrightness));
			RaisePropertyChanged(nameof(SnappingInterval));
		}
	}
}
