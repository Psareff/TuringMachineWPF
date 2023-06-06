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
using System.Data;

namespace TuringMachineGrandFinale
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TuringMachineModel turingMachine;

        DataTable tape;
        DataTable ptr;
        DataTable positions;

        DataRow str;
        DataRow pos;
        DataRow cur;

        public MainWindow()
        {
            turingMachine = new TuringMachineModel();

            InitializeComponent();

            tape = new DataTable();
            ptr = new DataTable();
            positions = new DataTable();

            str = tape.NewRow();
            cur = ptr.NewRow();
            pos = positions.NewRow();

            for (int i = 0; i < 20; i++)
            {
                tape.Columns.Add(new DataColumn());
                ptr.Columns.Add(new DataColumn());
                positions.Columns.Add(new DataColumn());

                pos[i] = i;
                str[i] = "_";
            }
            cur[0] = "^";

            tape.Rows.Add(str);
            ptr.Rows.Add(cur);
            positions.Rows.Add(pos);

            Tape.ItemsSource = tape.DefaultView;
            Positions.ItemsSource = positions.DefaultView;
            Ptr.ItemsSource = ptr.DefaultView;
        }
            
        private void Tape_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            Trace.WriteLine("Cell beginning edit");

        }

        private void Tape_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Trace.WriteLine("Cell edited");
        }

        private void UpToDateTape()
        {
            int i = 0;
            try
            {
            for (i = 0; i < 20; i++)
            if (str.ItemArray[i].ToString().Length > 1)
                throw new Exception("Each cell should be one char or space");
            Trace.WriteLine(turingMachine.CurPos);
            foreach (var item in str.ItemArray)
                Trace.Write(item.ToString() + " ");
            Trace.WriteLine("");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Invalid char {str.ItemArray[i].ToString()} at {i} place");
            }
        }

        private void Step_Click(object sender, RoutedEventArgs e)
        {
            UpToDateTape();
        }

        private void Tape_Left_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Ptr_Left_Click(object sender, RoutedEventArgs e)
        {
            if (turingMachine.CurPos != 0)
            {

                turingMachine.CurPos--;
                cur[turingMachine.CurPos] = "^";
                cur[turingMachine.CurPos + 1] = "";
            }
        }

        private void Tape_Right_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Ptr_Right_Click(object sender, RoutedEventArgs e)
        {
            if (turingMachine.CurPos != 19)
            {
                turingMachine.CurPos ++;
                cur[turingMachine.CurPos] = "^";
                cur[turingMachine.CurPos - 1] = "";
            }
            else
            {
                turingMachine.CurPos++;
                for (int i = 0; i < 20; i++)
                {

                }
                str.Le
            }
        }
    }
}
