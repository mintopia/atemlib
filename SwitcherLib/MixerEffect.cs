using BMDSwitcherAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherLib
{
    public class MixerEffect
    {
        private uint dTransitionFrames;

        public MixerEffect()
        {
        }
        public uint TransitionFrames
        {
            get
            {
                BMDSwitcherAPI.IBMDSwitcherTransitionMixParameters m_params =
                    (BMDSwitcherAPI.IBMDSwitcherTransitionMixParameters)m_mixEffectBlock1;
                m_params.GetRate(out dTransitionFrames); //m_mixEffectBlock1. out dTransitionFrames);
                return TransitionFrames;
            }
            set
            {
                BMDSwitcherAPI.IBMDSwitcherTransitionMixParameters m_params =
                    (BMDSwitcherAPI.IBMDSwitcherTransitionMixParameters)m_mixEffectBlock1;
                m_params.SetRate((uint)value);
            }

        }


        private IBMDSwitcherMixEffectBlock m_mixEffectBlock1;
    }
}
