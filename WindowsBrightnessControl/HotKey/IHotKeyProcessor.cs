using System.Windows.Input;

namespace WindowsBrightnessControl.HotKey
{
	public interface IHotKeyProcessor
	{
		void AddHotKey(ModifierKeys modifiers, Key keys);
		void RemoveHotKey(int id);
	}
}