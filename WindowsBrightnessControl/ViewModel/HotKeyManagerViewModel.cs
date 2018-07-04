using System.Collections.Generic;
using WindowsBrightnessControl.HotKey;

namespace WindowsBrightnessControl.ViewModel
{
	public class HotKeyManagerViewModel
	{
		private readonly IHotKeyProcessor _hotKeyProcessor;
		private readonly MainViewModel _mainViewModel;

		private SettingsViewModel Settings => _mainViewModel.Settings;

		private List<int> _hotKeyIds;

		public HotKeyManagerViewModel(IHotKeyProcessor hotKeyProcessor, MainViewModel mainViewModel)
		{
			_hotKeyProcessor = hotKeyProcessor;
			_hotKeyProcessor.StartHotKeyProcessor();
			_mainViewModel = mainViewModel;
			Settings.SettingsChanged += ConfigureHotKeys;
			ConfigureHotKeys();
		}

		public void ConfigureHotKeys()
		{
			ClearHotKeys();

			if (Settings.UseHotKeys)
			{
				int increaseBrightnessId = _hotKeyProcessor.AddHotKey(
					Settings.IncreaseBrightnessHotKey.Modifiers,
					Settings.IncreaseBrightnessHotKey.Key, () =>
				{
					_mainViewModel.IncreaseBrightness();
				});

				int decreaseBrightnessId = _hotKeyProcessor.AddHotKey(
					Settings.DecreaseBrightnessHotKey.Modifiers,
					Settings.DecreaseBrightnessHotKey.Key, () =>
				{
					_mainViewModel.DecreaseBrightness();
				});

				_hotKeyIds = new List<int>
				{
					increaseBrightnessId,
					decreaseBrightnessId
				};
			}
		}

		private void ClearHotKeys()
		{
			if (_hotKeyIds != null)
			{
				foreach (int id in _hotKeyIds)
				{
					_hotKeyProcessor.RemoveHotKey(id);
				}
				_hotKeyIds = null;
			}
		}
	}
}
