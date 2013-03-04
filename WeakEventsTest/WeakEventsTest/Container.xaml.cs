using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WeakEventsTest
{
    /// <summary>
    /// Interaction logic for Container.xaml
    /// </summary>
    public partial class Container : UserControl, IDisposable, IWeakEventListener
    {
        public Container()
        {
            InitializeComponent();

            MyWeakEventManager.AddListener(App.Current, this);
        }

        private void AddButton()
        {
            var button = new Button() { Content = "Click to stop" };
            button.Click += (o, args) => Dispose();

            stackPanel.Children.Add(button);
        }

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(MyWeakEventManager))
            {
                AddButton();
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            MyWeakEventManager.RemoveListener(App.Current, this);
        }
    }
}
