namespace WindowsBrightnessControl.ViewModel
{
	public class ValueViewModel<T> : ObservableObject
	{
		private T _value;
		public T Value
		{
			get { return _value; }
			set
			{
				_value = value;
				RaisePropertyChanged(nameof(Value));
			}
		}

		public ValueViewModel(T value)
		{
			_value = value;
		}
	}
}
