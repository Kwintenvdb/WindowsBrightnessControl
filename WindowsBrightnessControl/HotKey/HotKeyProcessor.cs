using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Interop;
using WindowsBrightnessControl.WinAPI;

namespace WindowsBrightnessControl.HotKey
{
	public class HotKeyProcessor : IHotKeyProcessor
	{
		public const int WM_HOTKEY = 0x312;

		private readonly HwndSource _windowHandleSource;
		private readonly HashSet<int> _hotKeyIds = new HashSet<int>();
		private IHotKeyHandler _handler;

		public HotKeyProcessor(IntPtr windowHandle)
		{
			_windowHandleSource = HwndSource.FromHwnd(windowHandle);
		}

		public void RegisterHandler(IHotKeyHandler handler)
		{
			_handler = handler;
		}

		public void StartHotKeyProcessor()
		{
			_windowHandleSource.AddHook(HwndHook);
		}

		public bool RegisterHotKey(int id, ModifierKeys modifiers, Key keys)
		{
			uint virtualKeys = (uint)KeyInterop.VirtualKeyFromKey(keys);
			if (User32.RegisterHotKey(_windowHandleSource.Handle, id, (uint)modifiers, virtualKeys))
			{
				return _hotKeyIds.Add(id);
			}
			return false;
		}

		public bool UnregisterHotKey(int id)
		{
			if (_hotKeyIds.Remove(id))
			{
				return User32.UnregisterHotKey(_windowHandleSource.Handle, id);
			}
			return false;
		}

		public void RemoveHotKey(int id)
		{
			if (_hotKeyIds.Remove(id))
			{
				User32.UnregisterHotKey(_windowHandleSource.Handle, id);
			}
		}

		private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			if (msg == WM_HOTKEY)
			{
				int id = wParam.ToInt32();
				if (_handler != null && _hotKeyIds.Contains(id))
				{
					handled = _handler.HandleHotKey(id);
				}
			}
			return IntPtr.Zero;
		}
	}
}
