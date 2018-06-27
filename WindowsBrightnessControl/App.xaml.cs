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

			// UI and ViewModels
			var window = new MainWindow();
			var windowHandle = new WindowInteropHelper(window).EnsureHandle();
			
			// Services
			IMonitorService service = new MonitorService();
			ISettingsProvider settingsProvider = new SettingsProvider();
			IHotKeyProcessor hotKeyProcessor = new HotKeyProcessor(windowHandle);

			// Set window DataContext and show.
			window.DataContext = new MainViewModel(service, hotKeyProcessor, settingsProvider);
			window.Show();
		}
	}
}
