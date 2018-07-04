using HotKeyInputControlLibrary;

namespace WindowsBrightnessControl.Model
{
	public struct Settings
	{
		// Brightness slider settings
		public bool SnapBrightness;
		public int SnappingInterval;
		public bool UseMouseWheel;
		// Use intervals?

		// Application settings
		public bool RunOnStartUp;
		public bool UseHotKeys;

		// Hotkeys
		public HotKeyData IncreaseBrightnessHotKey;
		public HotKeyData DecreaseBrightnessHotKey;
	}
}
