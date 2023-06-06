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
using System.Diagnostics;

namespace TuringMachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow1 : Window
    {
        public MainWindow1()
        {
            InitializeComponent();
        }
        public void HandleTapeClick(object sender, MouseButtonEventArgs e)
        {
            Trace.WriteLine("Ping");
            Trace.WriteLine(sender);
            var item = sender as ListViewItem;
            Trace.WriteLine(item);
            if (item != null)
                Trace.WriteLine(item.Content);
        }
    }
}
