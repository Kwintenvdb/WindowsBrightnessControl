using System;
using System.Threading.Tasks;
using NUnit.Framework;
using WindowsBrightnessControl.Util;

namespace UnitTests
{
	[TestFixture]
	public class ThrottlerTest
	{
		[Test]
		public void ThrottleFunction()
		{
			var throttler = new Throttler(1.0);

			int counter = 0;
			Action action = () =>
			{
				counter++;
			};

			bool result = throttler.TryExecute(action);
			Assert.IsTrue(result);
			result = throttler.TryExecute(action);
			Assert.IsFalse(result);

			Task.Delay(2000).Wait();
			result = throttler.TryExecute(action);
			Assert.IsTrue(result);

			Assert.AreEqual(2, counter);
		}
	}
}
