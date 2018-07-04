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
				SnapBrightness = userSettings.SnapBrightness,
				SnappingInterval = userSettings.SnappingInterval,
				UseMouseWheel = userSettings.UseMouseWheel,
				UseHotKeys = userSettings.UseHotKeys,
				RunOnStartUp = userSettings.RunOnStartUp,
				IncreaseBrightnessHotKey = userSettings.IncreaseBrightnessHotKey,
				DecreaseBrightnessHotKey = userSettings.DecreaseBrightnessHotKey
			};
		}

		public void SaveSettings(Settings settings)
		{
			var userSettings = UserSettings;
			userSettings.SnapBrightness = settings.SnapBrightness;
			userSettings.SnappingInterval = settings.SnappingInterval;
			userSettings.UseMouseWheel = settings.UseMouseWheel;
			userSettings.UseHotKeys = settings.UseHotKeys;
			userSettings.RunOnStartUp = settings.RunOnStartUp;
			userSettings.IncreaseBrightnessHotKey = settings.IncreaseBrightnessHotKey;
			userSettings.DecreaseBrightnessHotKey = settings.DecreaseBrightnessHotKey;
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
