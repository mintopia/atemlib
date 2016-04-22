using BMDSwitcherAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherLib
{
    public class SwitcherAuxPort
    {
        public long ID;
        public string Label;
        public string Name;
        public long Source;

        public SwitcherAuxPort()
        {
        }

        public SwitcherAuxPort(IBMDSwitcherInput input, long ID, long Source)
        {

        }
        

    }
}
