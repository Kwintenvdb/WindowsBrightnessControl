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
			IMonitorService monitorService = new MonitorService();
			IHotKeyProcessor hotKeyProcessor = new HotKeyProcessor(windowHandle);
			ISettingsProvider settingsProvider = new SettingsProvider();
			IStartupService startupService = new RegistryStartupService();
			IDialogService dialogService = new DialogService();

			// Set window DataContext and show.
			window.DataContext = new MainViewModel(monitorService, hotKeyProcessor, settingsProvider, startupService,
				dialogService);
			window.Show();
		}
	}
}
