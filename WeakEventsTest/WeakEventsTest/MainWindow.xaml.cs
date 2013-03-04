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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RaiseEventButton.Click += RaiseEventButton_Click;

            AddComponents();
        }

        void RaiseEventButton_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).Click();
        }

        private void AddComponents()
        {
            var container = new Container();
            stackPanel.Children.Add(container);

            container = new Container();
            stackPanel.Children.Add(container);

            container = new Container();
            stackPanel.Children.Add(container);

            container = new Container();
            stackPanel.Children.Add(container);

            container = new Container();
            stackPanel.Children.Add(container);
        }
    }
}
