using Microsoft.Win32;
using System.Reflection;

namespace WindowsBrightnessControl.Service
{
	// TODO: Create implementation for adding app shortcut to Startup folder which is less harmful...

	public class RegistryStartupService : IStartupService
	{
		public bool AppRunsOnStartup
		{
			get
			{
				using (RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY, true))
				{
					string value = key.GetValue(KEY_NAME) as string;
					return value == AssemblyLocation;
				}
			}
		}

		private string AssemblyLocation => Assembly.GetEntryAssembly().Location.ToString();

		private const string REGISTRY_KEY = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
		private const string KEY_NAME = "WindowsBrightnessControl";

		public void RunAppOnStartup(bool runOnStartup)
		{
			if (AppRunsOnStartup != runOnStartup)
			{
				using (RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY, true))
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
