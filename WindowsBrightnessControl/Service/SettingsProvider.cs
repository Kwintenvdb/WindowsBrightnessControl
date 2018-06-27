using System;
using WindowsBrightnessControl.Model;

namespace WindowsBrightnessControl.Service
{
	public class SettingsProvider : ISettingsProvider
	{
		public event Action SettingsChanged;

		private Properties.Settings UserSettings => Properties.Settings.Default;

		public Settings GetSettings()
		{
			var userSettings = UserSettings;
			return new Settings()
			{
				RunOnStartUp = userSettings.RunOnStartUp,
				SnapBrightness = userSettings.SnapBrightness,
				SnappingInterval = userSettings.SnappingInterval,
				UseHotKeys = userSettings.UseHotKeys
			};
		}

		public void SaveSettings(Settings settings)
		{
			var userSettings = UserSettings;
			userSettings.RunOnStartUp = settings.RunOnStartUp;
			userSettings.SnapBrightness = settings.SnapBrightness;
			userSettings.SnappingInterval = settings.SnappingInterval;
			userSettings.UseHotKeys = settings.UseHotKeys;
			userSettings.Save();

			SettingsChanged.Invoke();
		}

		public void ResetSettings()
		{
			UserSettings.Reset();
			SettingsChanged?.Invoke();
		}
	}
}
