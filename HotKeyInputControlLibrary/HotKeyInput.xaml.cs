using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HotKeyInputControlLibrary
{
	public partial class HotKeyInput : UserControl
	{
		public static readonly DependencyProperty HotKeyProperty = DependencyProperty.Register(
			nameof(HotKey), typeof(HotKeyData), typeof(HotKeyInput),
			new PropertyMetadata(default(HotKeyData), OnHotKeyDataChanged));

		public HotKeyData HotKey
		{
			get => (HotKeyData)GetValue(HotKeyProperty);
			set => SetValue(HotKeyProperty, value);
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

			// When Alt is pressed, SystemKey is used instead.
			if (key == Key.System)
			{
				key = e.SystemKey;
			}

			HotKey = new HotKeyData(modifiers, key);
		}

		private static void OnHotKeyDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var input = (HotKeyInput)d;
			input.textBox.Text = $"{input.HotKey.Modifiers} + {input.HotKey.Key}";
		}
	}
}
