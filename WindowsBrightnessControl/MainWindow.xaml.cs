using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using WindowsBrightnessControl.HotKey;
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

		private IMonitorService _monitorService;
		private MonitorViewModel _monitorVm;

		public MainWindow()
        {
            InitializeComponent();

			// Monitor interface...
			_monitorService = new MonitorService();
			var monitors = _monitorService.GetPhysicalMonitors();
			_monitorVm = new MonitorViewModel(monitors.First(), _monitorService);
			DataContext = _monitorVm;

			// Settings interface...
			var settingsProvider = new SettingsProvider();
			var settings = settingsProvider.GetSettings();
			var settingsVm = new SettingsViewModel(settings);
        }

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			var handle = new WindowInteropHelper(this).Handle;

			// Use interface...
			var hotKeyProcessor = new HotKeyProcessor();
			hotKeyProcessor.StartHotKeyProcessor(handle);

			// Setup hotkeys...
			hotKeyProcessor.AddHotKey(ModifierKeys.Alt, Key.F10, () =>
			{
				_monitorVm.Brightness -= 5;
			});
		}
	}
}
