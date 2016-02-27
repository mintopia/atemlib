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
        public string[] GetMacro(uint index)
        {
            this.Connect();
            string macroName;
            string macroDescription;
            int isValid;
            string isMacro;
            IBMDSwitcherMacroPool switcherMacroPool = (IBMDSwitcherMacroPool)this.switcher;
            switcherMacroPool.GetDescription(index, out macroDescription);
            switcherMacroPool.GetName(index, out macroName);
            switcherMacroPool.IsValid(index, out isValid);
            if (isValid == 1)
            {
                isMacro = "true";
            }
            else { 
                    isMacro = "False";
            }
            return new string[] { macroName, macroDescription, isMacro };
        }
        public void RunMacro(uint index)
        {
            //this.Connect();
            IBMDSwitcherMacroControl switcherMacroControl = (IBMDSwitcherMacroControl)this.switcher;
            switcherMacroControl.Run(index);
         }


    }
}
