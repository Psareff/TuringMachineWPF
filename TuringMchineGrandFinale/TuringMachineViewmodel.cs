using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace TuringMachineGrandFinale
{
    internal class TuringMachineViewmodel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        TuringMachineModel turing_machine;
        public string Tape 
        { 
            get
            {
                return "11010101";
            }
            set
            {

            }
        }

        public TuringMachineViewmodel()
        {
            turing_machine = new TuringMachineModel();
        }
    }
}
