﻿using System;
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
        }

        private void Tape_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            Trace.WriteLine("Cell beginning edit");


        }

        private void Tape_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Trace.WriteLine($"Value {str[e.Column.DisplayIndex]} at absolute {e.Column.DisplayIndex} and relative {pos[e.Column.DisplayIndex]}" );
            turingMachine.tape_dictionary[Convert.ToInt32(pos[e.Column.DisplayIndex])] = str.ItemArray[Convert.ToInt32(e.Column.DisplayIndex)].ToString();

            Trace.WriteLine(str.ItemArray[Convert.ToInt32(e.Column.DisplayIndex)].ToString());
            Trace.WriteLine(turingMachine.tape_dictionary[Convert.ToInt32(pos[e.Column.DisplayIndex])]);
            //Trace.WriteLine($"Cell {(relativeMove > e.Column.DisplayIndex ? e.Column.DisplayIndex - (relativeMove - 20): e.Column.DisplayIndex)} edited");
            //turingMachine.tape_dictionary[relativeMove > e.Column.DisplayIndex ? relativeMove -e.Column.DisplayIndex : e.Column.DisplayIndex] = str[e.Column.DisplayIndex].ToString();
        }

        private void UpToDateTape()
        {
            foreach (var v in turingMachine.tape_dictionary)
                Trace.WriteLine(v);
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
            if (absoluteShift > 0)
                absoluteShift--;
            relativeShift--;
            turingMachine.CurPos--;
            if (!turingMachine.tape_dictionary.ContainsKey(relativeShift))
                turingMachine.tape_dictionary.Add(relativeShift, "_");

            Trace.WriteLine("Relative :" + relativeShift + "; Absolute: " + absoluteShift);
            cur[absoluteShift] = "^";
            if (absoluteShift >= 19)
                cur[19] = " ";
            cur[absoluteShift + 1] = " ";

            if (relativeShift <= Convert.ToInt32(pos.ItemArray[absoluteShift]))
                for (int i = 0; i < 20; i++)
                    pos[i] = relativeShift - absoluteShift + i;

        }

        private void Tape_Right_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Ptr_Right_Click(object sender, RoutedEventArgs e)
        {
            if (absoluteShift < 19)
                absoluteShift++;
            relativeShift++;
            turingMachine.CurPos++;

            if (!turingMachine.tape_dictionary.ContainsKey(relativeShift))
                turingMachine.tape_dictionary.Add(relativeShift, "_");
            Trace.WriteLine("Relative :" + relativeShift + "; Absolute: " + absoluteShift);
            cur[absoluteShift] = "^";
            if (absoluteShift <= 0)
                cur[1] = " ";
            cur[absoluteShift - 1] = " ";
            if (relativeShift >= Convert.ToInt32(pos.ItemArray[absoluteShift]))
                for (int i = 0; i < 20; i++)
                    pos[i] = relativeShift - absoluteShift + i;

        }
    }
}
