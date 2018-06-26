using WindowsBrightnessControl.Model;

namespace WindowsBrightnessControl.Service
{
	public interface ISettingsProvider
	{
		Settings GetSettings();
		void SaveSettings(Settings settings);
	}
}
