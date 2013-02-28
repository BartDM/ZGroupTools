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
    public partial class Container : UserControl, IWeakEventListener
    {
        public Container()
        {
            InitializeComponent();

            MyWeakEventManager.AddListener(App.Current, this);
        }

        void Container_NewEvent(object sender, EventArgs e)
        {
            stackPanel.Children.Add(new Button());
        }

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(MyWeakEventManager))
            {
                Container_NewEvent(sender, e);
                return true;
            }
            return false;
        }
    }
}
