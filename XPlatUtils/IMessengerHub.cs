using System;

namespace XPlatUtils
{
	/// <summary>
	/// Messenger hub responsible for taking subscriptions/publications and delivering of messages.
	/// </summary>
	public interface IMessengerHub
	{
		/// <summary>
		/// Subscribe to a message type with the given destination and delivery action.
		/// </summary>
		/// <typeparam name="TMessage">Type of message</typeparam>
		/// <param name="deliveryAction">Action to invoke when message is delivered</param>
		/// <returns>MessageSubscription used to unsubscribing</returns>
		void Subscribe<TMessage>(Action<TMessage> deliveryAction) where TMessage : Message;

		/// <summary>
		/// Unsubscribe from a particular message type.
		/// </summary>
		/// <typeparam name="TMessage">Type of message</typeparam>
		/// <param name="deliveryAction">Action to invoke when message is delivered</param>
		void Unsubscribe<TMessage>(Action<TMessage> deliveryAction) where TMessage : Message;

		/// <summary>
		/// Publish a message to any subscribers
		/// </summary>
		/// <typeparam name="TMessage">Type of message</typeparam>
		/// <param name="message">Message to deliver</param>
		void Publish<TMessage>(TMessage message) where TMessage : Message;
	}
}

