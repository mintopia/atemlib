using BMDSwitcherAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherLib
{
    public class ColorInput
    {
        public long ID;
        public double Hue;
        public double Saturation;
        public double Luma;

        public ColorInput()
        {
        }
        public ColorInput(IBMDSwitcherInputColor color, long ID)
        {

        }
    }
}
