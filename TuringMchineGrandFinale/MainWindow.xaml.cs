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
using System.IO;

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

        int relativeShift = 0, absoluteShift = 0;

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

                turingMachine.tape_dictionary.Add(i, "_");
                pos[i] = i;
                str[i] = turingMachine.tape_dictionary[i];
            }
            cur[0] = "^";

            tape.Rows.Add(str);
            ptr.Rows.Add(cur);
            positions.Rows.Add(pos);

            Tape.ItemsSource = tape.DefaultView;
            Positions.ItemsSource = positions.DefaultView;
            Ptr.ItemsSource = ptr.DefaultView;

            /*
            turingMachine.AddRule(new Rule("q00->q11R"));
            turingMachine.AddRule(new Rule("q10->q10R"));
            turingMachine.AddRule(new Rule("q01->q01R"));
            turingMachine.AddRule(new Rule("q11->q11R"));
            turingMachine.AddRule(new Rule("q0_->q0_R"));
            turingMachine.AddRule(new Rule("q1_->q1_R"));

            turingMachine.AddRule(new Rule("q0_->q1xR"));
            turingMachine.AddRule(new Rule("q0x->q0xR"));
            turingMachine.AddRule(new Rule("q1_->q0_R"));
            turingMachine.AddRule(new Rule("q1x->q1xR"));
            */

        }

        private void UpToDateTape()
        {
            try
            {
                for (int i = 0; i < str.ItemArray.Length; i++)
                {
                    turingMachine.tape_dictionary[Convert.ToInt32(pos.ItemArray[i])] = str.ItemArray[i].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Step_Click(object sender, RoutedEventArgs e)
        {

            UpToDateTape();
            str[absoluteShift] = turingMachine.Process(turingMachine.tape_dictionary[relativeShift]);
            UpToDateTape();
           
            switch (turingMachine.curBehavior)
            {
                case Behavior.left:
                    MoveLeft();
                    break;
                case Behavior.right: 
                    MoveRight();
                    break;
                default:
                    break;
            }
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            UpToDateTape();
        }

        private void Ptr_Left_Click(object sender, RoutedEventArgs e)
        {
            UpToDateTape();
            MoveLeft();
        }

        private void Ptr_Right_Click(object sender, RoutedEventArgs e)
        {
            UpToDateTape();
            MoveRight();
        }

        private void MoveLeft()
        {
            if (absoluteShift > 0)
                absoluteShift--;
            relativeShift--;
            turingMachine.CurPos--;

            if (!turingMachine.tape_dictionary.ContainsKey(relativeShift))
                turingMachine.tape_dictionary.Add(relativeShift, "_");

            cur[absoluteShift] = "^";
            if (absoluteShift >= 19)
                cur[19] = "";
            cur[absoluteShift + 1] = "";

            if (relativeShift <= Convert.ToInt32(pos.ItemArray[absoluteShift]))
            {
                for (int i = 0; i < 20; i++)
                    pos[i] = relativeShift - absoluteShift + i;
                for (int i = 0; i < 20; i++)
                    str[i] = turingMachine.tape_dictionary[relativeShift - absoluteShift + i];
            }
        }

        private void Alphabet_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddRule_Click(object sender, RoutedEventArgs e)
        {
            Rule r = new Rule(Rule.Text);
            turingMachine.AddRule(r);
        }

        private void SaveRules_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllLines("test_out.txt", Array.ConvertAll(turingMachine.rules, r => r.));
        }

        private void LoadRules_Click(object sender, RoutedEventArgs e)
        {
            var lines = File.ReadAllLines("test_in.txt");
            for (var i = 0; i < lines.Length; i += 1)
            {
                Rule rule = new Rule(lines[i]);
                turingMachine.AddRule(rule);
            }
        }

        private void MoveRight()
        {
            if (absoluteShift < 19)
                absoluteShift++;
            relativeShift++;
            turingMachine.CurPos++;

            if (!turingMachine.tape_dictionary.ContainsKey(relativeShift))
                turingMachine.tape_dictionary.Add(relativeShift, "_");


            cur[absoluteShift] = "^";
            if (absoluteShift <= 0)
                cur[1] = "";
            cur[absoluteShift - 1] = "";

            if (relativeShift >= Convert.ToInt32(pos.ItemArray[absoluteShift]))
            {
                for (int i = 0; i < 20; i++)
                    pos[i] = relativeShift - absoluteShift + i;
                for (int i = 0; i < 20; i++)
                    str[i] = turingMachine.tape_dictionary[relativeShift - absoluteShift + i];
            }
        }

    }
}
