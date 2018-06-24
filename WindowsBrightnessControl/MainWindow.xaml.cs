using System.Linq;
using System.Threading;
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

			var service = new MonitorService();
			var monitors = service.GetPhysicalMonitors();
			var vm = new MonitorViewModel(monitors.First(), service);
			DataContext = vm;

			//var t = new Thread(() =>
			//{
			//	var sw = System.Diagnostics.Stopwatch.StartNew();
			//	while (true)
			//	{
			//		var elapsed = sw.Elapsed.TotalSeconds;
			//		var delta = elapsed / 5d;
			//		vm.Brightness = (uint)(100 * delta);
			//	}
			//});
			//t.Start();
        }
    }
}
