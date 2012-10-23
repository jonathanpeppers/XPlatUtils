using System;

namespace XPlatUtils
{
	/// <summary>
	/// Base class for messages that provides weak refrence storage of the sender
	/// </summary>
	public abstract class Message
	{
		/// <summary>
		/// Gets the original sender of the message
		/// </summary>
		public object Sender {
			get;
			private set;
		}

		/// <summary>
		/// Initializes a new instance of the MessageBase class.
		/// </summary>
		/// <param name="sender">Message sender (usually "this")</param>
		public Message (object sender)
		{
			if (sender == null)
				throw new ArgumentNullException ("sender");

			Sender = sender;
		}
	}
}

