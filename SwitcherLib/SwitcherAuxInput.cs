using BMDSwitcherAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherLib
{
    public class SwitcherAuxInput
    {
        public long ID;
        public string Label;
        public string Name;
        public long Source;

        public SwitcherAuxInput()
        {
        }

        public SwitcherAuxInput(IBMDSwitcherInput input, long ID, long Source)
        {

        }
        

    }
}
