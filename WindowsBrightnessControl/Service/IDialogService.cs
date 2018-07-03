namespace WindowsBrightnessControl.Service
{
	public delegate void DialogResultHandler(bool ok);

	public interface IDialogService
	{
		void ShowDialog(string title, string message, DialogResultHandler resultHandler);
	}
}