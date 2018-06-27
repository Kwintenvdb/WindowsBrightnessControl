using System.Windows;
using System.Windows.Interop;
using WindowsBrightnessControl.HotKey;
using WindowsBrightnessControl.Service;
using WindowsBrightnessControl.View;
using WindowsBrightnessControl.ViewModel;

namespace WindowsBrightnessControl
{
    public partial class App : Application
    {
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			// Services
			IHotKeyProcessor hotKeyProcessor = new HotKeyProcessor();
			IMonitorService service = new MonitorService();
			ISettingsProvider settingsProvider = new SettingsProvider();

			// UI and ViewModels
			var window = new MainWindow();
			var windowHandle = new WindowInteropHelper(window).EnsureHandle();
			hotKeyProcessor.StartHotKeyProcessor(windowHandle);

			var mainViewModel = new MainViewModel(service, hotKeyProcessor, settingsProvider);

			window.DataContext = mainViewModel;
			window.Show();
		}
	}
}
