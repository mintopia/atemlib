using BMDSwitcherAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherLib
{
    public class MediaPlayer
    {
        public uint Still;

        public MediaPlayer()
        {
        }
        public MediaPlayer(IBMDSwitcherMediaPlayer player, uint index)
        {
            _BMDSwitcherMediaPlayerSourceType type;
            
            player.GetSource(out type, out this.Still);
            if (type == _BMDSwitcherMediaPlayerSourceType.bmdSwitcherMediaPlayerSourceTypeStill)
            {

            }


        }

    }
}
