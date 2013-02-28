using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WeakEventsTest
{
    /// <summary>
    /// Provides a generic weak event listener which can be used to listen to events without a strong reference
    /// being made to the subscriber.
    /// </summary>
    /// <typeparam name="TEventArgs">
    /// The type of <see cref="EventArgs"/> accepted by the event handler.
    /// </typeparam>
    public class WeakEventListener<TEventArgs> : IWeakEventListener
        where TEventArgs : EventArgs
    {
        private readonly EventHandler<TEventArgs> _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakEventListener{TEventArgs}"/> class.
        /// </summary>
        /// <param name="handler">The handler for the event.</param>
        public WeakEventListener(EventHandler<TEventArgs> handler)
        {
            _handler = handler;
        }

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            TEventArgs eventArgs = e as TEventArgs;

            if (null == eventArgs)
                return false;

            _handler(sender, eventArgs);

            return true;
        }
    }
}
