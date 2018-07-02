using System.Windows;
using WindowsBrightnessControl.ViewModel;

namespace WindowsBrightnessControl.View
{
    public partial class MainWindow : BrightnessWindow
    {
		//public static readonly DependencyProperty IsWindowVisibleProperty = DependencyProperty.Register("IsWindowVisible",
		//	typeof(bool), typeof(MainWindow), new PropertyMetadata(default(bool)));

		//public bool IsWindowVisible
		//{
		//	get => (bool)GetValue(IsWindowVisibleProperty);
		//	set => SetValue(IsWindowVisibleProperty, value);
		//}

		//private MainViewModel MainViewModel
		//{
		//	get { return (MainViewModel)DataContext; }
		//}

		//private bool _settingsShowing;

		public MainWindow()
        {
            InitializeComponent();

			//int marginTop = 80;
			//this.Top = SystemParameters.WorkArea.Height - this.ActualHeight - marginTop;
			////this.Top = 100;
			//int marginLeft = 30;
			//this.Left = SystemParameters.WorkArea.Width - this.Width - marginLeft;
		}

		//public void ShowSettingsWindow(object sender, RoutedEventArgs args)
		//{
		//	if (!_settingsShowing)
		//	{
		//		var window = new SettingsWindow();
		//		window.DataContext = MainViewModel.GetEditingSettingsViewModel();
		//		window.Show();
		//		window.Closed += (o, e) => _settingsShowing = false;
		//		_settingsShowing = true;
		//	}
		//}

		//private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		//{
		//	if (e.OldValue == e.NewValue) return;

		//	var window = (MainWindow)d;
		//	bool visible = (bool)e.NewValue;
		//	System.Console.WriteLine("Window visible: " + visible);
		//}
	}
}
