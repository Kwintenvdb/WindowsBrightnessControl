namespace WindowsBrightnessControl.Service
{
	public interface IStartupService
	{
		bool AppRunsOnStartup { get; }
		/// <summary>
		/// Configure whether the app should run on Windows startup.
		/// </summary>
		void RunAppOnStartup(bool runOnStartup);
	}
}
