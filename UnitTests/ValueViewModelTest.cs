using NUnit.Framework;
using WindowsBrightnessControl.ViewModel;

namespace UnitTests
{
	[TestFixture]
	public class ValueViewModelTest
	{
		[Test]
		public void AssignValue()
		{
			int value = 10;
			var vm = new ValueViewModel<int>(value);

			Assert.AreEqual(value, vm.Value);

			int newValue = 20;
			vm.Value = newValue;
			Assert.AreEqual(newValue, vm.Value);
		}

		[Test]
		public void HandleChangedValue()
		{
			int value = 10;
			int changedValue = 20;
			var vm = new ValueViewModel<int>(value);

			int numValueChanged = 0;

			vm.ValueChanged += (newValue) =>
			{
				Assert.AreEqual(newValue, changedValue);
				++numValueChanged;
				Assert.AreEqual(1, numValueChanged);
			};

			vm.Value = changedValue;
			// ValueChanged should not be raised if value is unchanged.
			vm.Value = changedValue;
		}
	}
}
