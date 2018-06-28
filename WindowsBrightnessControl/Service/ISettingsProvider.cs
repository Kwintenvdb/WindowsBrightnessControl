using System;
using WindowsBrightnessControl.Model;

namespace WindowsBrightnessControl.Service
{
	public interface ISettingsProvider
	{
		event Action SettingsChanged;

		/// <summary>
		/// Returns a new instance of currently saved settings.
		/// </summary>
		Settings GetSettings();
		/// <summary>
		/// Saves and stores the settings on disk.
		/// </summary>
		void SaveSettings(Settings settings);
		/// <summary>
		/// Revert to default settings.
		/// </summary>
		void ResetSettings();
	}
}
