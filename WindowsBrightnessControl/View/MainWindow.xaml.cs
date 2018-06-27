using System.Windows;
using WindowsBrightnessControl.ViewModel;

namespace WindowsBrightnessControl.View
{
    public partial class MainWindow : Window
    {
		public Command ShowSettingsCommand { get; private set; }

		private MainViewModel MainViewModel
		{
			get { return (MainViewModel)DataContext; }
		}

		public MainWindow()
        {
            InitializeComponent();
			ShowSettingsCommand = new Command(ShowSettingsWindow);

			int marginTop = 80;
			this.Top = SystemParameters.WorkArea.Height - this.ActualHeight - marginTop;
			//this.Top = 100;
			int marginLeft = 30;
			this.Left = SystemParameters.WorkArea.Width - this.Width - marginLeft;
		}

		public void ShowSettingsWindow()
		{
			var window = new SettingsWindow();
			window.DataContext = MainViewModel.GetEditingSettingsViewModel();
			window.Show();
		}
	}
}
