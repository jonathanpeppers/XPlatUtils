using System;
using NUnit.Framework;
using MonoTouch.Foundation;

namespace XPlatUtils.Tests
{
	/// <summary>
	/// This is a test demonstrating Messenger vs NSNotificationCenter
	/// - Messenger is the winner, ~5ms vs ~271ms
	/// </summary>
	[TestFixture]
	public class MessengerSpeedTest
	{
		const int count = 100;

		private class TestMessage : Message
		{
			public TestMessage (object sender)
				: base(sender)
			{
				
			}
		}

		[Test]
		public void MessengerTest ()
		{
			var hub = new MessengerHub();
			for (int i = 0; i < count; i++) {
				hub.Subscribe<TestMessage>(_ => {
					//do nothing
				});
			}

			for (int i = 0; i < count; i++) {
				hub.Publish (new TestMessage(this));
			}
		}

		[Test]
		public void NotificationTest ()
		{
			var center = NSNotificationCenter.DefaultCenter;
			var key = new NSString("TEST");

			for (int i = 0; i < count; i++) {
				center.AddObserver(key, _ => {
					//do nothing
				});
			}

			for (int i = 0; i < count; i++) {
				center.PostNotificationName (key, new NSObject());
			}
		}
	}
}

