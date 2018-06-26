using System.Windows;

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
			var window = new SettingsWindow();
			window.Show();
		}
	}
}
