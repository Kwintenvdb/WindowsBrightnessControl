using WindowsBrightnessControl.Model;

namespace WindowsBrightnessControl.ViewModel
{
	public class SettingsViewModel : ObservableObject
	{
		private Settings _settings;

		public SettingsViewModel(Settings settings)
		{
			_settings = settings;
		}
	}
}
