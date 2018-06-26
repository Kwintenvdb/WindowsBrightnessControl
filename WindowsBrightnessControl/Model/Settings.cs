﻿namespace WindowsBrightnessControl.Model
{
	// Use application settings? Might be easier.
	public class Settings
	{
		public bool UseHotKeys { get; set; } = true;

		// Use intervals?
		public bool SnapBrightness { get; set; } = true;
		public int SnappingInterval { get; set; } = 5;

		// Application settings
		public bool RunOnStartUp { get; set; } = true;
	}
}
