using System;
using WindowsBrightnessControl.Util;

namespace WindowsBrightnessControl.HotKey
{
	public class HotKeyAction
	{
		public int Id { get; set; }
		private readonly Action _action;
		private readonly Throttler _throttler;

		public HotKeyAction(int id, Action action)
		{
			Id = id;
			_action = action;
			_throttler = new Throttler(0.04);
		}

		public bool TryExecuteAction()
		{
			return _throttler.TryExecute(_action);
		}
	}
}
