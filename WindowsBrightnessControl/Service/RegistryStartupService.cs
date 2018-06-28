using Microsoft.Win32;
using System.Reflection;

namespace WindowsBrightnessControl.Service
{
	// TODO: Create implementation for adding app shortcut to Startup folder which is less harmful...

	public class RegistryStartupService : IStartupService
	{
		public bool AppRunsOnStartup => false;

		private string AssemblyLocation => Assembly.GetEntryAssembly().Location.ToString();

		private const string KEY_NAME = "WindowsBrightnessControl";

		public void RunAppOnStartup(bool runOnStartup)
		{
			//if (AppRunsOnStartup != runOnStartup)
			{
				using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
				{
					if (runOnStartup)
					{
						key.SetValue(KEY_NAME, AssemblyLocation);
					}
					else
					{
						key.DeleteValue(KEY_NAME, false);
					}
				}
			}
		}
	}
}
