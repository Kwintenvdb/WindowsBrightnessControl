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
		/// Registers a hotkey handler.
		/// </summary>
		void RegisterHandler(IHotKeyHandler handler);

		/// <summary>
		/// Adds a hotkey with the system.
		/// </summary>
		bool RegisterHotKey(int id, ModifierKeys modifiers, Key keys);

		/// <summary>
		/// Unregisters a hotkey from the system.
		/// </summary>
		bool UnregisterHotKey(int id);
	}
}