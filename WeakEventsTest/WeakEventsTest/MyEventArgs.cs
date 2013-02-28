using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WeakEventsTest
{
    public class MyEventArgs : RoutedEventArgs
    {
        public MyEventArgs(RoutedEvent routedEvent)
        {
            RoutedEvent = routedEvent;
        }
    }
}
