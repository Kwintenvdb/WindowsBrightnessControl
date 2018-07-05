using WindowsBrightnessControl.Service;

namespace WindowsBrightnessControl.ViewModel
{
	public class HotKeyManagerViewModel
	{
		private readonly IHotKeyService _hotKeyService;
		private readonly MainViewModel _mainViewModel;

		private SettingsViewModel Settings => _mainViewModel.Settings;

		public HotKeyManagerViewModel(IHotKeyService hotKeyService, MainViewModel mainViewModel)
		{
			_hotKeyService = hotKeyService;
			_mainViewModel = mainViewModel;
			Settings.SettingsChanged += ConfigureHotKeys;
			ConfigureHotKeys();
		}

		public void ConfigureHotKeys()
		{
			ClearHotKeys();

			if (Settings.UseHotKeys)
			{
				_hotKeyService.AddHotKey(
					Settings.IncreaseBrightnessHotKey.Modifiers,
					Settings.IncreaseBrightnessHotKey.Key, () =>
				{
					_mainViewModel.IncreaseBrightness();
				});

				_hotKeyService.AddHotKey(
					Settings.DecreaseBrightnessHotKey.Modifiers,
					Settings.DecreaseBrightnessHotKey.Key, () =>
				{
					_mainViewModel.DecreaseBrightness();
				});
			}
		}

		private void ClearHotKeys()
		{
			_hotKeyService.ClearHotKeys();
		}
	}
}
