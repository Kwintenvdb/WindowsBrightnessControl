using System;

namespace WindowsBrightnessControl.Util
{
	public class Throttler
	{
		private readonly TimeSpan _throttleTimeSpan;
		private DateTime _lastExecuteDate;

		public Throttler(double throttleTimeInSeconds)
		{
			_throttleTimeSpan = TimeSpan.FromSeconds(throttleTimeInSeconds);
			_lastExecuteDate = DateTime.MinValue;
		}

		public bool TryExecute(Action action)
		{
			if (CanExecute())
			{
				action?.Invoke();
				_lastExecuteDate = DateTime.Now;
				return true;
			}

			return false;
		}

		private bool CanExecute()
		{
			return (DateTime.Now - _lastExecuteDate) >= _throttleTimeSpan;
		}
	}
}
