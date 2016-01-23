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
        public IList<MediaPlayer> GetMediaPlayers()
        {
            IList<MediaPlayer> list = new List<MediaPlayer>();

            IntPtr mediaPlayerIteratorPtr;
            Guid mediaIteratorIID = typeof(IBMDSwitcherMediaPlayerIterator).GUID;
            this.switcher.CreateIterator(ref mediaIteratorIID, out mediaPlayerIteratorPtr);
            IBMDSwitcherMediaPlayerIterator mediaPlayerIterator = (IBMDSwitcherMediaPlayerIterator)Marshal.GetObjectForIUnknown(mediaPlayerIteratorPtr);

            IBMDSwitcherMediaPlayer mediaPlayer;
            mediaPlayerIterator.Next(out mediaPlayer);
            int num1 = 1;
            while (mediaPlayer != null)
            {
                //MediaPlayer mediaPlayer = new MediaPlayer(mediaPlayers, index);
                list.Add(new MediaPlayer());
                _BMDSwitcherMediaPlayerSourceType type;
                uint index;
                mediaPlayer.GetSource(out type, out index);
                if (type == _BMDSwitcherMediaPlayerSourceType.bmdSwitcherMediaPlayerSourceTypeStill)
                {
                    int num2 = (int)index + 1;

                }
                num1++;
                mediaPlayerIterator.Next(out mediaPlayer);
            }
            return list;
        }
        /// <summary>
        /// 
        /// </summary>

    }
}
