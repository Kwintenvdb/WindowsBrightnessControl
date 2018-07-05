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
			ISettingsProvider settingsProvider = new SettingsProvider();
			IStartupService startupService = new RegistryStartupService();
			IDialogService dialogService = new DialogService();

			// HotKeys
			IHotKeyProcessor hotKeyProcessor = new HotKeyProcessor(windowHandle);
			IHotKeyService hotKeyService = new HotKeyService(hotKeyProcessor);

			// Set window DataContext and show.
			var vm = new MainViewModel(monitorService, hotKeyService, settingsProvider, startupService, dialogService);
			window.DataContext = vm;
			window.Visibility = Visibility.Collapsed;
		}
	}
}
