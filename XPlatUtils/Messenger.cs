using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace XPlatUtils {
    /// <summary>
    /// Messenger hub responsible for taking subscriptions/publications and delivering of messages.
    /// TODO: measure performance impact of weak references and implement
    /// </summary>
    public sealed class MessengerHub : IMessengerHub {
        Dictionary<Type, List<Delegate>> messages = new Dictionary<Type, List<Delegate>> ();

        public void Subscribe<TMessage> (Action<TMessage> deliveryAction) where TMessage : Message
        {
            if (deliveryAction == null) {
                throw new ArgumentNullException ("deliveryAction");
            }

            List<Delegate> list;
            if (!messages.TryGetValue (typeof (TMessage), out list)) {
                messages [typeof (TMessage)] =
                        list = new List<Delegate> ();
            }
            list.Add (deliveryAction);
        }

        public void Unsubscribe<TMessage> (Action<TMessage> deliveryAction) where TMessage : Message
        {
            if (deliveryAction == null) {
                throw new ArgumentNullException ("deliveryAction");
            }

            List<Delegate> list;
            if (messages.TryGetValue (typeof (TMessage), out list)) {
                list.Remove (deliveryAction);
            }
        }

        public void Publish<TMessage> (TMessage message) where TMessage : Message
        {
            if (message == null) {
                throw new ArgumentNullException ("message");
            }

            List<Delegate> list;
            if (messages.TryGetValue (typeof (TMessage), out list)) {
                //Call ToArray() in case someone unsubscribes during the iteration
                foreach (Action<TMessage> subscription in list.ToArray ()) {
                    subscription (message);
                }
            }
        }
    }
}