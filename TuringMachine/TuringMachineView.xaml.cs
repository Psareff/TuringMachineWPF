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
using System.Windows.Shapes;
using System.Threading;
using System.Diagnostics;


namespace TuringMachine
{
    /// <summary>
    /// Interaction logic for TuringMachine.xaml
    /// </summary>
    public partial class TuringMachineView : Window
    { 
        public TuringMachineView()
        {
            InitializeComponent();
        }

        private void AddRule_Click(object sender, RoutedEventArgs e)
        {
            char[] c = Tape.Text.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = (this.DataContext as MainWindowViewModel).Process(c[i]);
                Thread.Sleep(1000);
                Tape.Text = new string(c);
            }
        }
    }
}
