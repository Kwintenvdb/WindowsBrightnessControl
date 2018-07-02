using System.Windows;
using WindowsBrightnessControl.ViewModel;

namespace WindowsBrightnessControl.View
{
    public class BrightnessWindow : Window
    {
		public static readonly DependencyProperty IsWindowVisibleProperty = DependencyProperty.Register("IsWindowVisible",
			typeof(bool), typeof(BrightnessWindow), new PropertyMetadata(default(bool), OnWindowVisibleChanged));

		public bool IsWindowVisible
		{
			get => (bool)GetValue(IsWindowVisibleProperty);
			set => SetValue(IsWindowVisibleProperty, value);
		}

		private MainViewModel MainViewModel
		{
			get { return (MainViewModel)DataContext; }
		}

		private bool _settingsShowing;

		public BrightnessWindow()
        {
		}

		public void ShowSettingsWindow(object sender, RoutedEventArgs args)
		{
			if (!_settingsShowing)
			{
				var window = new SettingsWindow();
				window.DataContext = MainViewModel.GetEditingSettingsViewModel();
				window.Show();
				window.Closed += (o, e) => _settingsShowing = false;
				_settingsShowing = true;
			}
		}

		private static void OnWindowVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.OldValue == e.NewValue) return;

			var window = (MainWindow)d;
			bool visible = (bool)e.NewValue;
			window.OnWindowVisibleChanged(visible);
			//System.Console.WriteLine("Window visible: " + visible);
			//window.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
		}

		protected virtual void OnWindowVisibleChanged(bool visible)
		{
		}
	}
}
