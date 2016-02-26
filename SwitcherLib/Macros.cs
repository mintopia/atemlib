using BMDSwitcherAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherLib
{
    public partial class Switcher
    {
        

        public uint GetMacroCount()
        {
            this.Connect();
            uint macroCount;
            IBMDSwitcherMacroPool switcherMacroPool = (IBMDSwitcherMacroPool)this.switcher;
            switcherMacroPool.GetMaxCount(out macroCount);
            return macroCount;
        }
        public string GetMacro(uint index)
        {
            this.Connect();
            string macroName;
            IBMDSwitcherMacroPool switcherMacroPool = (IBMDSwitcherMacroPool)this.switcher;
            switcherMacroPool.GetDescription(index, out macroName);
            return macroName;
        }
        public string RunMacro(uint index)
        {
            this.Connect();
            string status;
            IBMDSwitcherMacroControl switcherMacroControl = (IBMDSwitcherMacroControl)this.switcher;
            switcherMacroControl.Run(index);
            status = "started";
            return status;
        }

    }
}
