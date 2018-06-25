using System;
using System.Windows.Input;

namespace WindowsBrightnessControl.HotKey
{
	public interface IHotKeyProcessor
	{
		/// <summary>
		/// Starts listening for hotkey events. Requires a valid Window Handle.
		/// </summary>
		void StartHotKeyProcessor(IntPtr windowHandle);
		/// <summary>
		/// Adds a hotkey mapped to a specified action.
		/// </summary>
		/// <returns>The id of the created hotkey.</returns>
		int AddHotKey(ModifierKeys modifiers, Key keys, Action action);
		/// <summary>
		/// Remove a hotkey given its id.
		/// </summary>
		void RemoveHotKey(int id);
	}
}