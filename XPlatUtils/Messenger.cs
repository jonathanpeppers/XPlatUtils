using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace XPlatUtils
{
	/// <summary>
	/// Messenger hub responsible for taking subscriptions/publications and delivering of messages.
	/// NOTE: uses WeakReferences, so forgetting to unsubscribe will not cause memory leaks
	/// </summary>
	public sealed class Messenger
	{
		Dictionary<Type, List<WeakReference>> messages = new Dictionary<Type, List<WeakReference>> ();

		/// <summary>
		/// Subscribes an action to a message of a particular type
		/// </summary>
		public void Subscribe<TMessage> (Action<TMessage> deliveryAction) where TMessage : Message
		{
			if (deliveryAction == null) {
				throw new ArgumentNullException ("deliveryAction");
			}

			List<WeakReference> list;
			if (!messages.TryGetValue (typeof(TMessage), out list)) {
				messages [typeof(TMessage)] =
                        list = new List<WeakReference> ();
			}
			list.Add (new WeakReference (deliveryAction, false));
		}
        
		/// <summary>
		/// Unsubscribes from messages of a particular type
		/// </summary>
		public void Unsubscribe<TMessage> (Action<TMessage> deliveryAction) where TMessage : Message
		{
			if (deliveryAction == null) {
				throw new ArgumentNullException ("deliveryAction");
			}

			List<WeakReference> list;
			if (messages.TryGetValue (typeof(TMessage), out list)) {
				WeakReference subscription;
				for (int i = 0; i < list.Count; i++) {
					subscription = list [i];
					if ((Action<TMessage>)subscription.Target == deliveryAction) {
						list.RemoveAt (i);
						break;
					}
				}
			}
		}

		/// <summary>
		/// Publishes a message, which is sent out to all subscribers
		/// </summary>
		public void Publish<TMessage> (TMessage message) where TMessage : Message
		{
			if (message == null) {
				throw new ArgumentNullException ("message");
			}

			List<WeakReference> list;
			if (messages.TryGetValue (typeof(TMessage), out list)) {
				//Call ToArray() in case someone unsubscribes during the iteration
				foreach (WeakReference subscription in list.ToArray ()) {
					if (subscription.IsAlive) {
						Action<TMessage> action = subscription.Target as Action<TMessage>;
						if (action != null)
							action (message);
					} else {
						list.Remove (subscription);
					}
				}
			}
		}
	}

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