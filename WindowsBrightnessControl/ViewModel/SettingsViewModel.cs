using WindowsBrightnessControl.Model;
using WindowsBrightnessControl.Service;

namespace WindowsBrightnessControl.ViewModel
{
	public class SettingsViewModel : ObservableObject
	{
		public Settings Settings { get; private set; }
		public Command SaveSettingsCommand { get; private set; }

		private readonly ISettingsProvider _settingsProvider;

		public SettingsViewModel(ISettingsProvider settingsProvider)
		{
			_settingsProvider = settingsProvider;
			Settings = settingsProvider.GetSettings();
			// Add settings dirty check func for CanExecute.
			SaveSettingsCommand = new Command(SaveSettings);
		}

		public void SaveSettings()
		{
			_settingsProvider.SaveSettings(Settings);
		}
	}
}
