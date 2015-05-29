using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMDSwitcherAPI;

namespace SwitcherLib.Callbacks
{
    class Stills : IBMDSwitcherStillsCallback
    {
        private Upload upload;

        public Stills(Upload upload)
        {
            this.upload = upload;
        }

        public void Notify(_BMDSwitcherMediaPoolEventType eventType, IBMDSwitcherFrame frame, int index)
        {
            Log.Debug(String.Format("Stills Callback: {0}", eventType.ToString()));
            _BMDSwitcherMediaPoolEventType mediaPoolEventType = eventType;

            if (mediaPoolEventType != _BMDSwitcherMediaPoolEventType.bmdSwitcherMediaPoolEventTypeTransferCompleted)
            {
                return;
            }
            this.upload.TransferCompleted();
        }
    }
}
