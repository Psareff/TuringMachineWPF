using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;


namespace TuringMachineGrandFinale
{
    internal class Rule
    { 
        public string curQ, nextQ;
        public string curSymb, nextSymb;
        public Behavior behavior;

        Regex regex = new Regex(@"q(\w+)(\w)->q(\w+)(\w)(L|R|S)");

        public Rule(string rule_str)
        {
            if (regex.IsMatch(rule_str))
            {
                string[] s = regex.Split(rule_str);
                curQ = s[1];
                curSymb = s[2];
                nextQ = s[3];
                nextSymb = s[4];
                behavior = (Behavior)s[5][0];
            }
            else throw new ArgumentException("Invalid rule pushed");
        }
    }
}
