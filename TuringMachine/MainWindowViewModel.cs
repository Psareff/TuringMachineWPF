using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachine
{
    internal class MainWindowViewModel
    {
        public List<char> arr { get; set; }
        public List<char> curPos{ get; set; }

        public MainWindowViewModel()
        {
            arr = new List<char>();
            curPos = new List<char>();

            for (int i = 0; i < 41; i++)
            {
                arr.Add('1');
                if (i != 23)
                    curPos.Add(' ');
                else 
                    curPos.Add('⬇');
            }

        }
    }
}
