using BMDSwitcherAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherLib
{
    public class Switcher
    {
        protected IBMDSwitcher switcher;
        protected String deviceAddress;
        protected bool connected;

        public Switcher(string deviceAddress)
        {
            this.deviceAddress = deviceAddress;
        }

        public IBMDSwitcher GetSwitcher()
        {
            return this.switcher;
        }

        public void Connect()
        {
            if (this.connected)
            {
                return;
            }

            IBMDSwitcherDiscovery switcherDiscovery = new CBMDSwitcherDiscovery();
            _BMDSwitcherConnectToFailure failReason = 0;

            try
            {
                switcherDiscovery.ConnectTo(this.deviceAddress, out this.switcher, out failReason);
                this.connected = true;
            }
            catch (COMException ex)
            {
                switch (failReason)
                {
                    case _BMDSwitcherConnectToFailure.bmdSwitcherConnectToFailureIncompatibleFirmware:
                        throw new SwitcherLibException("Incompatible firmware");

                    case _BMDSwitcherConnectToFailure.bmdSwitcherConnectToFailureNoResponse:
                        throw new SwitcherLibException(String.Format("No response from {0}", this.deviceAddress));

                    default:
                        throw new SwitcherLibException(String.Format("Unknown Error: {0}", ex.Message));
                }
            }
            catch (Exception ex)
            {
                throw new SwitcherLibException(String.Format("Unable to connect to switcher: {0}", ex.Message));
            }
        }

        public String GetProductName()
        {
            this.Connect();
            String productName;
            this.switcher.GetProductName(out productName);
            return productName;
        }
        public String GetVideoMode()
        {
            this.Connect();
            _BMDSwitcherVideoMode videoMode;
            this.switcher.GetVideoMode(out videoMode);
            return videoMode.ToString();
        }
        public int GetVideoHeight()
        {
            this.Connect();
            _BMDSwitcherVideoMode videoMode;
            this.switcher.GetVideoMode(out videoMode);
            _BMDSwitcherVideoMode switcherVideoMode = videoMode;
            switch (switcherVideoMode)
            {
                case _BMDSwitcherVideoMode.bmdSwitcherVideoMode4KHDp2398:
                case _BMDSwitcherVideoMode.bmdSwitcherVideoMode4KHDp24:
                case _BMDSwitcherVideoMode.bmdSwitcherVideoMode4KHDp25:
                case _BMDSwitcherVideoMode.bmdSwitcherVideoMode4KHDp2997:
                    return 2160;

                case _BMDSwitcherVideoMode.bmdSwitcherVideoMode720p50:
                case _BMDSwitcherVideoMode.bmdSwitcherVideoMode720p5994:
                    return 720;

                case _BMDSwitcherVideoMode.bmdSwitcherVideoMode1080i50:
                case _BMDSwitcherVideoMode.bmdSwitcherVideoMode1080i5994:
                case _BMDSwitcherVideoMode.bmdSwitcherVideoMode1080p50:
                case _BMDSwitcherVideoMode.bmdSwitcherVideoMode1080p2398:
                case _BMDSwitcherVideoMode.bmdSwitcherVideoMode1080p24:
                case _BMDSwitcherVideoMode.bmdSwitcherVideoMode1080p25:
                case _BMDSwitcherVideoMode.bmdSwitcherVideoMode1080p2997:
                case _BMDSwitcherVideoMode.bmdSwitcherVideoMode1080p5994:
                    return 1080;

                default:
                    throw new SwitcherLibException(String.Format("Unsupported resolution: {0}", videoMode.ToString()));
            }
        }

        public int GetVideoWidth()
        {
            int videoHeight = this.GetVideoHeight();
            switch (videoHeight)
            {
                case 720:
                    return 1280;

                case 1080:
                    return 1920;

                case 2160:
                    return 3840;

                default:
                    throw new SwitcherLibException(String.Format("Unsupported video height: {0}", videoHeight.ToString()));
            }
        }

        public IList<MediaStill> GetStills()
        {
            IList<MediaStill> list = new List<MediaStill>();

            IBMDSwitcherMediaPool switcherMediaPool = (IBMDSwitcherMediaPool)this.switcher;

            IBMDSwitcherStills stills;
            switcherMediaPool.GetStills(out stills);

            uint count;
            stills.GetCount(out count);
            for (uint index = 0; index < count; index++)
            {
                MediaStill mediaStill = new MediaStill(stills, index);
                list.Add(mediaStill);
            }

            IntPtr mediaPlayerIteratorPtr;
            Guid mediaIteratorIID = typeof(IBMDSwitcherMediaPlayerIterator).GUID;
            this.switcher.CreateIterator(ref mediaIteratorIID, out mediaPlayerIteratorPtr);
            IBMDSwitcherMediaPlayerIterator mediaPlayerIterator = (IBMDSwitcherMediaPlayerIterator)Marshal.GetObjectForIUnknown(mediaPlayerIteratorPtr);

            IBMDSwitcherMediaPlayer mediaPlayer;
            mediaPlayerIterator.Next(out mediaPlayer);
            int num1 = 1;
            while (mediaPlayer != null)
            {
                _BMDSwitcherMediaPlayerSourceType type;
                uint index;
                mediaPlayer.GetSource(out type, out index);
                if (type == _BMDSwitcherMediaPlayerSourceType.bmdSwitcherMediaPlayerSourceTypeStill)
                {
                    int num2 = (int)index + 1;
                    foreach (MediaStill mediaStill in list)
                    {
                        if (mediaStill.Slot == num2)
                        {
                            mediaStill.MediaPlayer = num1;
                            break;
                        }
                    }
                }
                num1++;
                mediaPlayerIterator.Next(out mediaPlayer);
            }
            return list;
        }

        public IList<SwitcherInput> GetInputs()
        {
            IList<SwitcherInput> list = new List<SwitcherInput>();
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
            while (input != null)
            {
                string inputName;
                string inputLabel;
                string iPortType;
                long inputPortType;
                long inputId;

                input.GetInputId(out inputId);
                input.GetString(_BMDSwitcherInputPropertyId.bmdSwitcherInputPropertyIdLongName, out inputName);
                input.GetString(_BMDSwitcherInputPropertyId.bmdSwitcherInputPropertyIdShortName, out inputLabel);
                input.GetInt(_BMDSwitcherInputPropertyId.bmdSwitcherInputPropertyIdPortType, out inputPortType);
                //must be a better way of getting the port type as a string value - fix this hack when I know what I'm doing
                switch(inputPortType)
                {
                    case (long)_BMDSwitcherPortType.bmdSwitcherPortTypeExternal:
                        iPortType = "External Port";
                        break;
                    case (long)_BMDSwitcherPortType.bmdSwitcherPortTypeBlack:
                        iPortType = "Black Port";
                        break;
                    case (long)_BMDSwitcherPortType.bmdSwitcherPortTypeColorBars:
                        iPortType = "Color Bars Port";
                        break;
                    case (long)_BMDSwitcherPortType.bmdSwitcherPortTypeColorGenerator:
                        iPortType = "Color Generator Port";
                        break;
                    case (long)_BMDSwitcherPortType.bmdSwitcherPortTypeMediaPlayerFill:
                        iPortType = "Media Player Fill Port";
                        break;
                    case (long)_BMDSwitcherPortType.bmdSwitcherPortTypeMediaPlayerCut:
                        iPortType = "Media Player Cut Port";
                        break;
                    case (long)_BMDSwitcherPortType.bmdSwitcherPortTypeSuperSource:
                        iPortType = "Super Source Port";
                        break;
                    case (long)_BMDSwitcherPortType.bmdSwitcherPortTypeMixEffectBlockOutput:
                        iPortType = "ME Block Port";
                        break;

                    default:
                        throw new SwitcherLibException(String.Format("Unsupported port type: {0}", inputPortType.ToString()));

                }
                // Add items to list:
                list.Add(new SwitcherInput() {Name=inputName, ID = inputId, Label = inputLabel, PortType = iPortType});

                inputIterator.Next(out input);
            }

            return list;

        }
    }
}
