using System;
using System.Collections.Generic;
using System.Windows.Input;
using WindowsBrightnessControl.HotKey;

namespace WindowsBrightnessControl.Service
{
	public class HotKeyService : IHotKeyService, IHotKeyHandler
	{
		private readonly IHotKeyProcessor _processor;
		private readonly Dictionary<int, HotKeyAction> _hotKeys = new Dictionary<int, HotKeyAction>();

		public HotKeyService(IHotKeyProcessor processor)
		{
			_processor = processor;
			_processor.RegisterHandler(this);
			_processor.StartHotKeyProcessor();
		}

		public HotKeyAction AddHotKey(ModifierKeys modifiers, Key key, Action action)
		{
			int id = _hotKeys.Count;
			if (_processor.RegisterHotKey(id, modifiers, key))
			{
				var hotKey = new HotKeyAction(id, action);
				_hotKeys.Add(id, hotKey);
				return hotKey;
			}
			return null;
		}

		public bool RemoveHotKey(HotKeyAction hotKey)
		{
			if (_processor.UnregisterHotKey(hotKey.Id))
			{
				return _hotKeys.Remove(hotKey.Id);
			}
			return false;
		}

		public void ClearHotKeys()
		{
			foreach (var kvp in _hotKeys)
			{
				_processor.UnregisterHotKey(kvp.Key);
			}
			_hotKeys.Clear();
		}

		public bool HandleHotKey(int id)
		{
			if (_hotKeys.TryGetValue(id, out HotKeyAction hotKey))
			{
				return hotKey.TryExecuteAction();
			}
			return false;
		}
	}
}
