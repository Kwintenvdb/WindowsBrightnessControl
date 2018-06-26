using System.Windows;
using WindowsBrightnessControl.Service;
using WindowsBrightnessControl.ViewModel;

namespace WindowsBrightnessControl.View
{
    public partial class MainWindow : Window
    {
		public Command ShowSettingsCommand { get; private set; }

		public MainWindow()
        {
            InitializeComponent();
			ShowSettingsCommand = new Command(ShowSettingsWindow);
		}

		public void ShowSettingsWindow()
		{
			var settingsProvider = new SettingsProvider();
			var viewModel = new SettingsViewModel(settingsProvider);
			var window = new SettingsWindow();
			window.DataContext = viewModel;
			window.Show();
		}
	}
}
