using System;
using System.Linq;
using System.Windows;
using System.Windows.Interop;
using WindowsBrightnessControl.Service;
using WindowsBrightnessControl.ViewModel;
using WindowsBrightnessControl.WinAPI;

namespace TestSetBrightness
{
    public partial class MainWindow : Window
    {
		//Modifiers:
		private const uint MOD_NONE = 0x0000; //[NONE]
		private const uint MOD_ALT = 0x0001; //ALT
		private const uint MOD_CONTROL = 0x0002; //CTRL
		private const uint MOD_SHIFT = 0x0004; //SHIFT
		private const uint MOD_WIN = 0x0008; //WINDOWS
		
		//CAPS LOCK:
		private const uint VK_CAPITAL = 0x14;

		private const int HOTKEY_ID = 9000;

		public MainWindow()
        {
            InitializeComponent();

			// Monitor interface...
			var monitorService = new MonitorService();
			var monitors = monitorService.GetPhysicalMonitors();
			var vm = new MonitorViewModel(monitors.First(), monitorService);
			DataContext = vm;

			// Settings interface...
			var settingsProvider = new SettingsProvider();
			var settings = settingsProvider.GetSettings();
			var settingsVm = new SettingsViewModel(settings);
        }

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			var handle = new WindowInteropHelper(this).Handle;
			var source = HwndSource.FromHwnd(handle);
			source.AddHook(HwndHook);

			bool registered = User32.RegisterHotKey(handle, HOTKEY_ID, MOD_CONTROL, VK_CAPITAL);
		}

		private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			const int WM_HOTKEY = 0x0312;
			switch (msg)
			{
				case WM_HOTKEY:
					switch (wParam.ToInt32())
					{
						case HOTKEY_ID:
							Console.WriteLine("Hotkey pressed...");
							handled = true;
							break;
					}
					break;
			}
			return IntPtr.Zero;
		}
	}
}
