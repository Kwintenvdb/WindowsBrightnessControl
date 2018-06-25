using System;
using System.Windows.Input;

namespace WindowsBrightnessControl.HotKey
{
	public interface IHotKeyProcessor
	{
		void StartHotKeyProcessor(IntPtr windowHandle);
		void AddHotKey(ModifierKeys modifiers, Key keys, Action action);
		void RemoveHotKey(int id);
	}
}