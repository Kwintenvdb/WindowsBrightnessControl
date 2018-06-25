using WindowsBrightnessControl.Model;

namespace WindowsBrightnessControl.Service
{
	public class SettingsProvider : ISettingsProvider
	{
		public Settings GetSettings()
		{
			return new Settings();
		}
	}
}
