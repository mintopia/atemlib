using BMDSwitcherAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherLib
{
    public class SwitcherInput
    {
        public long ID;
        public string Label;
        public string Name;  
        //public Boolean PortType;


        public SwitcherInput()
        {
        }


        public SwitcherInput(IBMDSwitcherInput input, long ID)
        {
            
        }
    }
}
