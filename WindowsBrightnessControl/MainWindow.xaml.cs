using System.Linq;
using System.Windows;
using WindowsBrightnessControl.Service;
using WindowsBrightnessControl.ViewModel;

namespace TestSetBrightness
{
    public partial class MainWindow : Window
    {
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
    }
}
