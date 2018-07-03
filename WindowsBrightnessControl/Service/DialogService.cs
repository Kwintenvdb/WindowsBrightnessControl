using System.Windows;

namespace WindowsBrightnessControl.Service
{
	public class DialogService : IDialogService
	{
		public void ShowDialog(string title, string message, DialogResultHandler resultHandler)
		{
			var result = MessageBox.Show(message, title, MessageBoxButton.YesNo);
			resultHandler?.Invoke(result == MessageBoxResult.OK || result == MessageBoxResult.Yes);
		}
	}
}
