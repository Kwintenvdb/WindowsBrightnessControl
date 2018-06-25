using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Interop;
using WindowsBrightnessControl.WinAPI;

namespace WindowsBrightnessControl.HotKey
{
	public class HotKeyProcessor : IHotKeyProcessor
	{
		private class HotKey
		{
			public int Id { get; set; }
			public Action Action { get; set; }
		}

		private readonly HwndSource _windowHandleSource;
		private readonly Dictionary<int, HotKey> _hotKeys = new Dictionary<int, HotKey>();

		public HotKeyProcessor(IntPtr windowHandle)
		{
			_windowHandleSource = HwndSource.FromHwnd(windowHandle);
			_windowHandleSource.AddHook(HwndHook);
		}

		// Pass in some params...
		public void AddHotKey(ModifierKeys modifiers, Key keys)
		{
			// get an id for each hotkey.
			int id = 0x9000;
			uint virtualKeys = (uint)KeyInterop.VirtualKeyFromKey(Key.F10);
			var hotKey = new HotKey()
			{
				Id = id,
				Action = null
			};
			_hotKeys.Add(id, hotKey);
			User32.RegisterHotKey(_windowHandleSource.Handle, id, (uint)modifiers, virtualKeys);
		}

		public void RemoveHotKey(int id)
		{
			if (_hotKeys.Remove(id))
			{
				User32.UnregisterHotKey(_windowHandleSource.Handle, id);
			}
		}

		private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			const int WM_HOTKEY = 0x0312;
			if (msg == WM_HOTKEY)
			{
				int id = wParam.ToInt32();
				HotKey hotKey;
				if (_hotKeys.TryGetValue(id, out hotKey))
				{
					hotKey.Action?.Invoke();
					handled = true;
				}
			}
			return IntPtr.Zero;
		}
	}
}
