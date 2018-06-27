using System;
using System.Windows.Input;

namespace WindowsBrightnessControl.HotKey
{
	public interface IHotKeyProcessor
	{
		/// <summary>
		/// Starts listening for hotkey events.
		/// </summary>
		void StartHotKeyProcessor();
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