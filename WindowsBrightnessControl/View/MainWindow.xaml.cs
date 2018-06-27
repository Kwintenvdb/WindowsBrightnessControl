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

		private bool _settingsShowing;

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
			if (!_settingsShowing)
			{
				var window = new SettingsWindow();
				window.DataContext = MainViewModel.GetEditingSettingsViewModel();
				window.Show();
				window.Closed += (o, e) => _settingsShowing = false;
				_settingsShowing = true;
			}
		}
	}
}
