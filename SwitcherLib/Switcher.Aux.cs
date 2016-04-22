using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using BMDSwitcherAPI;

namespace SwitcherLib
{
    public partial class Switcher
    {
        public IList<SwitcherAuxPort> GetAuxInputs()
        {
            IList<SwitcherAuxPort> list = new List<SwitcherAuxPort>();
            long Source = 0;
            IBMDSwitcherInputIterator inputIterator = null;
            IntPtr inputIteratorPtr;
            Guid inputIteratorIID = typeof(IBMDSwitcherInputIterator).GUID;
            switcher.CreateIterator(ref inputIteratorIID, out inputIteratorPtr);
            if (inputIteratorPtr != null)
            {
                inputIterator = (IBMDSwitcherInputIterator)Marshal.GetObjectForIUnknown(inputIteratorPtr);
            }

            IBMDSwitcherInput input;
            inputIterator.Next(out input);
            int AUXCount = 0;
            while (input != null)
            {


                    long inputPortType;
                    string AuxName;
                    string AuxLabel;
                    long AuxId;
                    input.GetInputId(out AuxId);
                    input.GetString(_BMDSwitcherInputPropertyId.bmdSwitcherInputPropertyIdLongName, out AuxName);
                    input.GetString(_BMDSwitcherInputPropertyId.bmdSwitcherInputPropertyIdShortName, out AuxLabel);
                    input.GetInt(_BMDSwitcherInputPropertyId.bmdSwitcherInputPropertyIdPortType, out inputPortType);
                    if (inputPortType == (long)_BMDSwitcherPortType.bmdSwitcherPortTypeAuxOutput)
                    {
                        AUXCount++;
                        IBMDSwitcherInputAux WkAux = (IBMDSwitcherInputAux)input;
                        WkAux.GetInputSource(out Source);
                        list.Add(new SwitcherAuxPort() { Name = AuxName, ID = AuxId, Label = AuxLabel, Source = Source });

                    }

                inputIterator.Next(out input);
            }
            
            return list;
        }
        public void SetAuxInput(long AuxID, long InputID)
        {
            IBMDSwitcherInputIterator inputIterator = null;
            IntPtr inputIteratorPtr;
            Guid inputIteratorIID = typeof(IBMDSwitcherInputIterator).GUID;
            this.switcher.CreateIterator(ref inputIteratorIID, out inputIteratorPtr);
            if (inputIteratorPtr != null)
            {
                inputIterator = (IBMDSwitcherInputIterator)Marshal.GetObjectForIUnknown(inputIteratorPtr);
            }

            if (inputIterator != null)
            {
                IBMDSwitcherInput input;
                inputIterator.Next(out input);
                int AUXCount = 0;
                while (input != null)
                {
                    long inputPortType;
                    input.GetInt(_BMDSwitcherInputPropertyId.bmdSwitcherInputPropertyIdPortType, out inputPortType);

                    if (inputPortType == (long)_BMDSwitcherPortType.bmdSwitcherPortTypeAuxOutput)
                    {
                        AUXCount++;
                        if (AUXCount == AuxID)
                        {
                            IBMDSwitcherInputAux WkAux = (IBMDSwitcherInputAux)input;
                            WkAux.SetInputSource(InputID);
                            break;
                        }
                    }
                    inputIterator.Next(out input);
                }
            }
        }
    }
}
