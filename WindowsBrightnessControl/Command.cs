using System;
using System.Windows.Input;

namespace WindowsBrightnessControl
{
	public class Command : ICommand
	{
		public event EventHandler CanExecuteChanged;

		private readonly Action _execute;
		private readonly Func<bool> _canExecute;

		public Command(Action executeAction, Func<bool> canExecuteFunc = null)
		{
			_execute = executeAction;
			_canExecute = canExecuteFunc;
		}

		public bool CanExecute(object parameter)
		{
			if (_canExecute != null)
			{
				return _canExecute.Invoke();
			}
			return true;
		}

		public void Execute(object parameter)
		{
			if (_execute == null)
				throw new InvalidOperationException("Command must have non-null execute action.");
			_execute.Invoke();
		}

		public void RaiseCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, new EventArgs());
		}
	}
}
