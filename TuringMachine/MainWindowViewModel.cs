using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace TuringMachine
{
    enum Behavior { left = 'L', right = 'R', stop = 'S' }

    class Rule
    {
        public char NxtState;
        public char NxtChar;
        public Behavior behavior;
        public Rule (string unparsed_rule)
        {
            NxtState = unparsed_rule[6];
            NxtChar = unparsed_rule[7];
            behavior = (Behavior)unparsed_rule[8];
        }
        public char ApplyRule()
        {
            return NxtChar;
        }
    }

    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        string arr, curPos, alphabet;
        List<string> rules;
        Dictionary<(char, char ), Rule> rules_parsed;

        public char curQ;

        public string Arr 
        {
            get => arr;
            set
            {
                arr = value;
                OnPropertyChanged("arr");
            }
        }
        public string CurPos
        {
            get => curPos;
            set
            {
                curPos = value;
                OnPropertyChanged("curPos");
            }

        }
        public string Alphabet
        {
            get => alphabet;
            set
            {
                alphabet = value;
                OnPropertyChanged("alphabet");
            }
        }
        public List<string> Rules
        {
            get => rules;
            set
            {
                rules = value;
                OnPropertyChanged("rules");
            }
        }
        int ptr = 5;

        public MainWindowViewModel()
        {
            arr = "0000000000";
            curPos = "^";
            alphabet = "10_";
            // 1100101001
            // 0011010110
            rules = new List<string>() { 
                "q00->q11R", 
                "q01->q01R",
                "q10->q00R",
                "q11->q11R"};
            rules_parsed = new Dictionary<(char, char), Rule>();
            curQ = '0';
            foreach (var rule in rules)
            {
                rules_parsed.Add((rule[1], rule[2]), new Rule(rule));
                
            }

            curPos = curPos.Insert(0, string.Concat(Enumerable.Repeat(" ", ptr)));
        }

        public char Process(char ch)
        {
            curQ = rules_parsed[(curQ, ch)].NxtState;
            ch = rules_parsed[(curQ, ch)].ApplyRule();
            return ch;
            //return newch;
            //curPos = curPos.Insert(0, string.Concat(Enumerable.Repeat(" ", ptr)));
        }

        public void ProcessAll(string str)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            Trace.WriteLine("Property changed");
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
