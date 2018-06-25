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
			public HotKey(int id, Action action)
			{
				Id = id;
				Action = action;
			}

			public int Id { get; set; }
			public Action Action { get; set; }
		}

		private HwndSource _windowHandleSource;
		private readonly Dictionary<int, HotKey> _hotKeys = new Dictionary<int, HotKey>();

		public void StartHotKeyProcessor(IntPtr windowHandle)
		{
			_windowHandleSource = HwndSource.FromHwnd(windowHandle);
			_windowHandleSource.AddHook(HwndHook);
		}

		public int AddHotKey(ModifierKeys modifiers, Key keys, Action action)
		{
			int id = _hotKeys.Count;
			uint virtualKeys = (uint)KeyInterop.VirtualKeyFromKey(keys);
			var hotKey = new HotKey(id, action);
			_hotKeys.Add(id, hotKey);
			User32.RegisterHotKey(_windowHandleSource.Handle, id, (uint)modifiers, virtualKeys);
			return id;
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
