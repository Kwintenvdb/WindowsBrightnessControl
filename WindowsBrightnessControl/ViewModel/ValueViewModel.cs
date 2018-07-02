namespace WindowsBrightnessControl.ViewModel
{

	public class ValueViewModel<T> : ObservableObject
	{
		public delegate void ValueChangedHandler(T newValue);
		public event ValueChangedHandler ValueChanged;
		
		private T _value;
		public T Value
		{
			get => _value;
			set
			{
				if (SetField(ref _value, value))
				{
					ValueChanged?.Invoke(value);
				}
			}
		}

		public ValueViewModel(T value)
		{
			_value = value;
		}
	}
}
