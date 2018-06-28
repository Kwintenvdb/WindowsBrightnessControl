namespace WindowsBrightnessControl.Service
{
	public interface IStartupService
	{
		bool AppRunsOnStartup { get; }

		void RunAppOnStartup(bool runOnStartup);
	}
}
