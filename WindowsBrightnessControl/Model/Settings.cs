namespace WindowsBrightnessControl.Model
{
	public struct Settings
	{
		// Use intervals?
		public bool SnapBrightness;
		public int SnappingInterval;

		// Application settings
		public bool RunOnStartUp;
		public bool UseHotKeys;
	}
}
