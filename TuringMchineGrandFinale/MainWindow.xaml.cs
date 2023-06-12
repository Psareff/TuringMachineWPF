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
using Microsoft.Win32;

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
            var lines = File.ReadAllLines("Rules.cfg");
            for (var i = 0; i < lines.Length; i += 1)
            {
                Rule rule = new Rule(lines[i]);
                turingMachine.AddRule(rule);
            }

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
            DisplayRules();
        }

        private void DisplayRules()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var i in turingMachine.rules)
                sb.Append(i.str + "\n");
            RulesList.Text = sb.ToString();
        }

        private void UpToDateTape()
        {
            try
            {
                for (int i = 0; i < str.ItemArray.Length; i++)
                    turingMachine.tape_dictionary[Convert.ToInt32(pos.ItemArray[i])] = str.ItemArray[i].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpToDateView()
        {
            try
            {
                for (int i = 0; i < str.ItemArray.Length; i++)
                    str[i] = turingMachine.tape_dictionary[Convert.ToInt32(pos.ItemArray[i])];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Step_Click(object sender, RoutedEventArgs e)
        {

            try
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
            catch (Exception ex)
            {
                MessageBox.Show("No rule exists with this parameters");
            }
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            while (turingMachine.curBehavior !=  Behavior.stop)
            {
                try
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
                            return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No rule exists with this parameters");
                }
            }
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

        private void AddRule_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Rule r = new Rule(Rule.Text);
                turingMachine.AddRule(r);
                DisplayRules();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveRules_Click(object sender, RoutedEventArgs e)
        {
            //File.WriteAllLines("test_out.txt", Array.ConvertAll(turingMachine.rules, r => r.));
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            foreach (var i in turingMachine.rules)
            {
                File.WriteAllText(ofd.FileName, i.str);
            }
        }

        private void LoadRules_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            string[] lines;
            try
            {
                turingMachine.rules.Clear();
                lines = File.ReadAllLines(ofd.FileName);
                for (var i = 0; i < lines.Length; i++)
                {
                    Rule rule = new Rule(lines[i]);
                    turingMachine.AddRule(rule);
                }
                Trace.WriteLine(turingMachine.rules.Count);
                DisplayRules();

            }
            catch (Exception ex)
            {
            }

        }

        private void SaveState_Click(object sender, RoutedEventArgs e)
        {
            UpToDateTape();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(turingMachine.CurPos.ToString());

            foreach (var i in turingMachine.tape_dictionary)
                sb.Append(i.Value);
            sb.AppendLine();
            foreach (var i in turingMachine.tape_dictionary)
                sb.Append(i.Key + " ");
            sb.AppendLine();
            foreach (var i in turingMachine.rules)
                sb.AppendLine(i.str + " ");
            File.WriteAllText(ofd.FileName, sb.ToString());

        }

        private void OpenState_Click(object sender, RoutedEventArgs e)
        {
            UpToDateView();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            string[] lines = File.ReadAllLines(ofd.FileName);
            turingMachine.CurPos = Convert.ToInt32(lines[0]);

            var j = lines[2].Split(" ");

            turingMachine.tape_dictionary.Clear();

            for (int i = 0; i < lines[1].Length; i++)
                turingMachine.tape_dictionary.Add(Convert.ToInt32(j[i]), lines[1][i].ToString());
            turingMachine.rules.Clear();
            for (int i = 3; i < lines.Length; i++)
            {
                Rule rule = new Rule(lines[i]);
                turingMachine.AddRule(rule);
            }
            UpToDateView();
            DisplayRules();
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
