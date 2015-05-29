using SwitcherLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaUpload
{
    class MediaUpload
    {
        private static int Main(string[] args)
        {
            try
            {
                MediaUpload.ProcessArgs(args);
                return 0;
            }
            catch (SwitcherLibException ex)
            {
                Console.Error.WriteLine(ex.Message);
                return -1;
            }
        }

        private static void Help()
        {
            ConsoleUtils.Version();
            Console.Out.WriteLine();
            Console.Out.WriteLine("Usage: mediaupload.exe [options] <hostname> <slot> <filename>");
            Console.Out.WriteLine("Uploads an image to a BlackMagic ATEM switcher");
            Console.Out.WriteLine();
            Console.Out.WriteLine("Arguments:");
            Console.Out.WriteLine();
            Console.Out.WriteLine(" hostname        - The hostname or IP of the ATEM switcher");
            Console.Out.WriteLine(" slot            - The number of the media slot to upload to");
            Console.Out.WriteLine(" filename        - The filename of the image to upload");
            Console.Out.WriteLine();
            Console.Out.WriteLine("Options:");
            Console.Out.WriteLine();
            Console.Out.WriteLine(" -h, --help      - This help message");
            Console.Out.WriteLine(" -d, --debug     - Debug output");
            Console.Out.WriteLine(" -v, --version   - Version information");
            Console.Out.WriteLine(" -n, --name      - The name for the item in the media pool");
            Console.Out.WriteLine();
            Console.Out.WriteLine("Image Format:");
            Console.Out.WriteLine();
            Console.Out.WriteLine("The image must be the same resolution as the switcher. Accepted formats are BMP, JPEG, GIF, PNG and TIFF. Alpha channels are supported.");
        }

        private static void ProcessArgs(string[] args)
        {
            IList<string> args1 = new List<string>();
            string name = "";
            for (int index = 0; index < args.Length; index++)
            {
                switch (args[index])
                {
                    case "-h":
                    case "--help":
                    case "-?":
                    case "/?":
                    case "/h":
                    case "/help":
                        MediaUpload.Help();
                        return;

                    case "-v":
                    case "--version":
                    case "/v":
                    case "/version":
                        ConsoleUtils.Version();
                        return;

                    case "-d":
                    case "--debug":
                    case "/d":
                    case "/debug":
                        Log.CurrentLevel = Log.Level.Debug;
                        break;

                    case "-n":
                    case "--name":
                    case "/n":
                    case "/name":
                        if (index < args.Length)
                        {
                            name = args[index + 1];
                            index++;
                            break;
                        }
                        break;

                    default:
                        args1.Add(args[index]);
                        break;
                }
            }
            MediaUpload.Upload(name, args1);
        }

        private static void Upload(string name, IList<string> args)
        {
            if (args.Count < 3)
            {
                MediaUpload.Help();
                throw new SwitcherLibException("Invalid arguments");
            }

            Switcher switcher = new Switcher(args[0]);
            int slot = MediaUpload.GetSlot(args[1]);
            Log.Debug(String.Format("Switcher: {0}", switcher.GetProductName()));
            Log.Debug(String.Format("Resolution: {0}x{1}", switcher.GetVideoWidth().ToString(), switcher.GetVideoHeight().ToString()));
            args.RemoveAt(0);
            args.RemoveAt(0);

            string filename = String.Join(" ", args);
            Upload upload = new Upload(switcher, filename, slot);
            if (name != "")
            {
                upload.SetName(name);
            }
            upload.Start();
            while (upload.InProgress())
            {
                Log.Info(String.Format("Progress: {0}%", upload.GetProgress().ToString()));
                Thread.Sleep(100);
            }
        }

        private static int GetSlot(string arg)
        {
            try
            {
                return Convert.ToInt32(arg) - 1;
            }
            catch (Exception ex)
            {
                throw new SwitcherLibException(String.Format("Invalid slot: {0}", arg), ex);
            }
        }
    }
}
