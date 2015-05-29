using BMDSwitcherAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherLib
{
    public class MediaStill
    {
        public String Name;
        public String Hash;
        public int Slot;
        public int MediaPlayer;

        public MediaStill()
        {
        }

        public MediaStill(IBMDSwitcherStills stills, uint index)
        {
            BMDSwitcherHash hash;
            stills.GetHash(index, out hash);
            this.Hash = String.Join("", BitConverter.ToString(hash.data).Split('-'));
            stills.GetName(index, out this.Name);
            this.Slot = (int)index + 1;
        }

        public String ToCSV()
        {
            return String.Join(",", this.Slot.ToString(), "\"" + this.Name + "\"", "\"" + this.Hash + "\"", this.MediaPlayer.ToString());
        }
    }
}
