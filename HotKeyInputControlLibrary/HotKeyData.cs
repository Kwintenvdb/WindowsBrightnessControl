using System.Configuration;
using System.Windows.Input;

namespace HotKeyInputControlLibrary
{
	[SettingsSerializeAs(SettingsSerializeAs.Xml)]
	public class HotKeyData
	{
		public ModifierKeys Modifiers;
		public Key Key;

		public HotKeyData() : this(ModifierKeys.None, Key.None)
		{
		}

		public HotKeyData(ModifierKeys modifiers, Key key)
		{
			Modifiers = modifiers;
			Key = key;
		}
	}
}
