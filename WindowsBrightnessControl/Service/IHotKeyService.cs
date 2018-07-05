using System;
using System.Windows.Input;
using WindowsBrightnessControl.HotKey;

namespace WindowsBrightnessControl.Service
{
	public interface IHotKeyService
	{
		/// <summary>
		/// Add a hotkey mapped to an action.
		/// </summary>
		HotKeyAction AddHotKey(ModifierKeys modifiers, Key key, Action action);

		/// <summary>
		/// Remove and stop listening for a specific hotkey.
		/// </summary>
		bool RemoveHotKey(HotKeyAction hotKey);
		
		/// <summary>
		/// Remove and stop listening for all previously added hotkeys.
		/// </summary>
		void ClearHotKeys();
	}
}