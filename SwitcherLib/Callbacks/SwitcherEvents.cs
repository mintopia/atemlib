using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMDSwitcherAPI;

namespace SwitcherLib.Callbacks
{
    public delegate void SwitcherEventHandler(object sender, object args);

 
    public class MixEffectBlockMonitor : IBMDSwitcherMixEffectBlockCallback
    {
        // Events:
        public event SwitcherEventHandler ProgramInputChanged;
        public event SwitcherEventHandler PreviewInputChanged;
        public event SwitcherEventHandler TransitionFramesRemainingChanged;
        public event SwitcherEventHandler TransitionPositionChanged;
        public event SwitcherEventHandler InTransitionChanged;

        public MixEffectBlockMonitor()
        {
        }


        void IBMDSwitcherMixEffectBlockCallback.PropertyChanged(_BMDSwitcherMixEffectBlockPropertyId propId)
        {
            switch (propId)
            {
                case _BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdProgramInput:
                    if (ProgramInputChanged != null)
                        ProgramInputChanged(this, null);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdPreviewInput:
                    if (PreviewInputChanged != null)
                        PreviewInputChanged(this, null);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdTransitionFramesRemaining:
                    if (TransitionFramesRemainingChanged != null)
                        TransitionFramesRemainingChanged(this, null);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdTransitionPosition:
                    if (TransitionPositionChanged != null)
                        TransitionPositionChanged(this, null);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdInTransition:
                    if (InTransitionChanged != null)
                        InTransitionChanged(this, null);
                    break;
            }
        }

    }

    class InputMonitor : IBMDSwitcherInputCallback
    {
        // Events:
        public event SwitcherEventHandler LongNameChanged;

        private IBMDSwitcherInput m_input;
        public IBMDSwitcherInput Input { get { return m_input; } }

        public InputMonitor(IBMDSwitcherInput input)
        {
            m_input = input;
        }

        void IBMDSwitcherInputCallback.PropertyChanged(_BMDSwitcherInputPropertyId propId)
        {
            switch (propId)
            {
                case _BMDSwitcherInputPropertyId.bmdSwitcherInputPropertyIdLongName:
                    if (LongNameChanged != null)
                        LongNameChanged(this, null);
                    break;
            }
        }
    }
    class AuxMonitor : IBMDSwitcherInputAuxCallback
    {
        public event SwitcherEventHandler AuxSourceChanged;

        private int m_aux_number;
        public int AuxNumber { get { return m_aux_number; } }

        public AuxMonitor(int aux_number)
        {
            m_aux_number = aux_number;
        }

        void IBMDSwitcherInputAuxCallback.Notify(_BMDSwitcherInputAuxEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherInputAuxEventType.bmdSwitcherInputAuxEventTypeInputSourceChanged:
                    if (AuxSourceChanged != null)
                        AuxSourceChanged(this, null);
                    break;
            }
        }
    }
    class SwitcherCallback : IBMDSwitcherCallback
    {
        public event SwitcherEventHandler SwitcherDisconnected;

        void IBMDSwitcherCallback.Notify(_BMDSwitcherEventType eventType)
        {
            if (eventType == _BMDSwitcherEventType.bmdSwitcherEventTypeDisconnected)
            {
                if (SwitcherDisconnected != null)
                    SwitcherDisconnected(this, null);
            }
        }
    }

}
