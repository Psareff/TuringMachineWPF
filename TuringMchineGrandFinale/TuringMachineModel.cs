using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Shell;

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
        public Dictionary<int, string> tape_dictionary;
        int curPos = 0;
        public string curState;
        public Behavior curBehavior = Behavior.right;

        List<Rule> rules;
        public TuringMachineModel()
        {
            tape_dictionary = new Dictionary<int, string>();
            curState = "0";
            rules = new List<Rule>();
        }

        public string Process(string c)
        {
            Rule res = rules.First(r => (r.curSymb == c && r.curQ == curState));
            Trace.WriteLine($"q{res.curQ}{res.curSymb}->q{res.nextQ}{res.nextSymb}{res.behavior}");
            curState = res.nextQ;
            curBehavior = res.behavior;
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
