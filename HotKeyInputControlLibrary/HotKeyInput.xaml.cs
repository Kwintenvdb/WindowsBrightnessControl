using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HotKeyInputControlLibrary
{
	public partial class HotKeyInput : UserControl
	{
		public static readonly DependencyProperty ModifiersProperty = DependencyProperty.Register(
			nameof(Modifiers), typeof(ModifierKeys), typeof(HotKeyInput),
			new PropertyMetadata(ModifierKeys.None, OnKeyValueChanged));

		public static readonly DependencyProperty KeyProperty = DependencyProperty.Register(
			nameof(Key), typeof(Key), typeof(HotKeyInput),
			new PropertyMetadata(Key.None, OnKeyValueChanged));

		public ModifierKeys Modifiers
		{
			get => (ModifierKeys)GetValue(ModifiersProperty);
			set => SetValue(ModifiersProperty, value);
		}

		public Key Key
		{
			get => (Key)GetValue(KeyProperty);
			set => SetValue(KeyProperty, value);
		}

		public HotKeyInput()
		{
			InitializeComponent();
		}

		private void OnPreviewKeyDown(object sender, KeyEventArgs e)
		{
			e.Handled = true;

			var modifiers = e.KeyboardDevice.Modifiers;
			var key = e.Key;

			Modifiers = modifiers;
			Key = key;
		}

		private static void OnKeyValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var input = (HotKeyInput)d;
			input.textBox.Text = $"{input.Modifiers} + {input.Key}";
		}
	}
}
