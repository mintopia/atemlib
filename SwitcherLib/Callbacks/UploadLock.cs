using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMDSwitcherAPI;

namespace SwitcherLib.Callbacks
{
    class UploadLock : IBMDSwitcherLockCallback
    {
        private Upload upload;

        public UploadLock(Upload upload)
        {
            this.upload = upload;
        }

        public void Obtained()
        {
            Log.Debug("Still upload lock obtained");
            this.upload.LockCallback();
        }
    }
}
