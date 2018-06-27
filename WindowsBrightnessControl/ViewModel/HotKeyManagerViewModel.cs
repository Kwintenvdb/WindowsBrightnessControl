using System.Collections.Generic;
using System.Windows.Input;
using WindowsBrightnessControl.HotKey;

namespace WindowsBrightnessControl.ViewModel
{
	public class HotKeyManagerViewModel
	{
		private readonly IHotKeyProcessor _hotKeyProcessor;
		private readonly MainViewModel _mainViewModel;

		private MonitorViewModel Monitor => _mainViewModel.Monitor;
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
				int increaseBrightnessId = _hotKeyProcessor.AddHotKey(ModifierKeys.Alt, Key.F10, () =>
				{
					Monitor.Brightness += Settings.SnappingInterval;
				});

				int decreaseBrightnessId = _hotKeyProcessor.AddHotKey(ModifierKeys.Alt, Key.F9, () =>
				{
					Monitor.Brightness -= Settings.SnappingInterval;
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
			}
		}
	}
}
