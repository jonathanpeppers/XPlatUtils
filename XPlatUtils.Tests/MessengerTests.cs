using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace XPlatUtils.Tests {
    [TestFixture]
    public class MessengerTests {
        IMessenger messenger;

        #region TestClasses

        class TestMessage : Message {
            public TestMessage (object sender)
                : base (sender)
            {                           

            }
        }

        #endregion

        [SetUp]
        public void SetUp ()
        {
            messenger = new MessengerHub ();
        }

        [Test]
        public void SubscribeAndPublish ()
        {
            var message = new TestMessage (this);

            messenger.Subscribe<TestMessage> (m => {

                Assert.That (m, Is.EqualTo (message));
                Assert.That (m.Sender, Is.EqualTo (this));
            });

            messenger.Publish (message);
        }

        [Test]
        public void Unsubscribe ()
        {
            Action<TestMessage> action = _ => Assert.That (false, "This event should not fire!");

            messenger.Subscribe (action);
            messenger.Unsubscribe (action);
            messenger.Publish (new TestMessage (this));
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void NullSender ()
        {
            new TestMessage (null);
        }

        [Test, ExpectedException (typeof (ArgumentNullException))]
        public void NullSubscribe ()
        {
            messenger.Subscribe<TestMessage> (null);
        }

        [Test, ExpectedException (typeof (ArgumentNullException))]
        public void NullUnsubscribe ()
        {
            messenger.Unsubscribe<TestMessage> (null);
        }

        [Test, ExpectedException (typeof (ArgumentNullException))]
        public void NullPublish ()
        {
            messenger.Publish<TestMessage> (null);
        }
    }
}
