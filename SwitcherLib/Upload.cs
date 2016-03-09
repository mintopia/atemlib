using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using BMDSwitcherAPI;
using System.IO;
using System.Runtime.InteropServices;
using SwitcherLib.Callbacks;

namespace SwitcherLib
{
    public class Upload
    {
        private enum Status
        {
            NotStarted,
            Started,
            Completed,
        }

        private Upload.Status currentStatus;
        private String filename;
        private int uploadSlot;
        private String name;
        private Switcher switcher;
        private IBMDSwitcherFrame frame;
        private IBMDSwitcherStills stills;
        private IBMDSwitcherLockCallback lockCallback;

        public Upload(Switcher switcher, String filename, int uploadSlot)
        {
            this.switcher = switcher;
            this.filename = filename;
            this.uploadSlot = uploadSlot;
            //check
            
            if (!File.Exists(filename))
            {
                throw new SwitcherLibException(String.Format("{0} does not exist", filename));
            }

            this.switcher.Connect();
            this.stills = this.GetStills();

        }

        public bool InProgress()
        {
            return this.currentStatus == Upload.Status.Started;
        }

        public void SetName(String name)
        {
            this.name = name;
        }

        public int GetProgress()
        {
            if (this.currentStatus == Upload.Status.NotStarted)
            {
                return 0;
            }
            if (this.currentStatus == Upload.Status.Completed)
            {
                return 100;
            }

            double progress;
            this.stills.GetProgress(out progress);
            return (int)Math.Round(progress * 100.0);
        }

        public void Start()
        {
            this.currentStatus = Upload.Status.Started;
            this.frame = this.GetFrame();
            this.lockCallback = (IBMDSwitcherLockCallback)new UploadLock(this);
            this.stills.Lock(this.lockCallback);
            
        }
        public void StartBlocking()
        {
            this.currentStatus = Upload.Status.Started;
            this.frame = this.GetFrame();
            this.lockCallback = (IBMDSwitcherLockCallback)new UploadLock(this);
            this.stills.Lock(this.lockCallback);
            while (currentStatus != Status.Completed)
            {
                           }
        }

        protected IBMDSwitcherFrame GetFrame()
        {
            IBMDSwitcherMediaPool switcherMediaPool = (IBMDSwitcherMediaPool)this.switcher.GetSwitcher();
            IBMDSwitcherFrame frame;
            switcherMediaPool.CreateFrame(_BMDSwitcherPixelFormat.bmdSwitcherPixelFormat8BitARGB, (uint)this.switcher.GetVideoWidth(), (uint)this.switcher.GetVideoHeight(), out frame);
            IntPtr buffer;
            byte[] source;
            frame.GetBytes(out buffer);
            switch (Path.GetExtension(this.filename).ToLower())
            {
                case ".jpg":
                case ".png":
                case ".bmp":
                case ".gif":
                case ".tif":
                    source = this.ConvertImage();
                    break;
                case ".tga":
                    throw new SwitcherLibException(String.Format("TGA not supported yet for: {0}", this.filename));
                default:
                    throw new SwitcherLibException(String.Format("Unsupported file extension: {0}", Path.GetExtension(this.filename)));

            }
            //source = this.ConvertImage();
            Marshal.Copy(source, 0, buffer, source.Length);
            return frame;
        }

        protected byte[] ConvertImage()
        {
            try
            {
                Bitmap image = new Bitmap(this.filename);

                if (image.Width != this.switcher.GetVideoWidth() || image.Height != this.switcher.GetVideoHeight())
                {
                    throw new SwitcherLibException(String.Format("Image is {0}x{1} it needs to be the same resolution as the switcher", image.Width.ToString(), image.Height.ToString()));
                }

                byte[] numArray = new byte[image.Width * image.Height * 4];
                for (int index1 = 0; index1 < image.Width * image.Height; index1++)
                {
                    Color pixel = this.GetPixel(image, index1);
                    int index2 = index1 * 4;
                    numArray[index2] = pixel.B;
                    numArray[index2 + 1] = pixel.G;
                    numArray[index2 + 2] = pixel.R;
                    numArray[index2 + 3] = pixel.A;
                }
                return numArray;
            }
            catch (Exception ex)
            {
                throw new SwitcherLibException(ex.Message, ex);
            }
        }

        protected Color GetPixel(Bitmap image, int index)
        {
            int x = index % image.Width;
            int y = (index - x) / image.Width;
            return image.GetPixel(x, y);
        }

        protected IBMDSwitcherStills GetStills()
        {
            IBMDSwitcherMediaPool switcherMediaPool = (IBMDSwitcherMediaPool)this.switcher.GetSwitcher();
            IBMDSwitcherStills stills;
            switcherMediaPool.GetStills(out stills);
            return stills;
        }

        public void UnlockCallback()
        {
            this.currentStatus = Upload.Status.Completed;
        }

        public void LockCallback()
        {
            IBMDSwitcherStillsCallback callback = (IBMDSwitcherStillsCallback)new Stills(this);
            this.stills.AddCallback(callback);
            this.stills.Upload((uint)this.uploadSlot, this.GetName(), this.frame);
        }

        public void TransferCompleted()
        {
            Log.Debug("Completed upload");
            this.stills.Unlock(this.lockCallback);
            this.currentStatus = Upload.Status.Completed;
        }

        public String GetName()
        {
            if (this.name != null)
            {
                return this.name;
            }
            return Path.GetFileNameWithoutExtension(this.filename);
        }
    }
}
