using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WeakEventsTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public event EventHandler MyCustomEvent;

        public void Click()
        {
            MyCustomEvent.Invoke(this, new EventArgs());
        }
    }
}
