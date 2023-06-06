﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TuringMachineGrandFinale
{
    internal class TuringMachineModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        string tape;
        int curPos = 0;
        string curState;

        List<Rule> rules;
        public TuringMachineModel()
        {
            curState = "0";
            rules = new List<Rule>();
        }

        public char Process(char c)
        {
            Rule res = rules.First(
                r => (
                r.curSymb == c && 
                r.curQ == curState)
                );
            curState = res.nextQ;
            return res.nextSymb;
        }

        public void AddRule(Rule r)
        {
            if (rules.Contains(r))
                throw new ArgumentException("Rules list already contains this rule!");
            rules.Add(r);
        }
        public string Tape
        {
            get => tape;
            set
            {
                StringBuilder stringBuilder = new StringBuilder(value);
                tape = stringBuilder.ToString();
            }
        }
        public int CurPos
        {
            get => curPos;
            set => curPos = value;
        }
    }
}
