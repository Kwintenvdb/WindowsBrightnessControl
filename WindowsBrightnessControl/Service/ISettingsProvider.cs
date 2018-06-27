using System;
using WindowsBrightnessControl.Model;

namespace WindowsBrightnessControl.Service
{
	public interface ISettingsProvider
	{
		event Action SettingsChanged;

		Settings GetSettings();
		void SaveSettings(Settings settings);
		/// <summary>
		/// Revert to default settings.
		/// </summary>
		void ResetSettings();
	}
}
