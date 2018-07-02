using Microsoft.Win32;
using System.Reflection;

namespace WindowsBrightnessControl.Service
{
	// TODO: Create implementation for adding app shortcut to Startup folder which is less harmful...
	public class RegistryStartupService : IStartupService
	{
		private string AssemblyLocation => Assembly.GetEntryAssembly().Location.ToString();

		private const string REGISTRY_KEY = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
		private const string KEY_NAME = "WindowsBrightnessControl";

		public bool AppRunsOnStartup
		{
			get
			{
				using (RegistryKey key = OpenRegistryKey())
				{
					string value = key.GetValue(KEY_NAME) as string;
					return value == AssemblyLocation;
				}
			}
		}

		public void RunAppOnStartup(bool runOnStartup)
		{
			if (AppRunsOnStartup != runOnStartup)
			{
				using (RegistryKey key = OpenRegistryKey())
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

		private RegistryKey OpenRegistryKey()
		{
			return Registry.CurrentUser.OpenSubKey(REGISTRY_KEY, true);
		}
	}
}
