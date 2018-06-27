namespace WindowsBrightnessControl.ViewModel
{
	public class ValueViewModel<T> : ObservableObject
	{
		private T _value;
		public T Value
		{
			get => _value;
			set => SetField(ref _value, value);
		}

		public ValueViewModel(T value)
		{
			_value = value;
		}
	}
}
