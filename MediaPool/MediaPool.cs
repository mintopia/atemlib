using Newtonsoft.Json;
using SwitcherLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MediaPool
{
    class MediaPool
    {
        private enum Format
        {
            Text,
            JSON,
            XML,
            CSV,
        }

        static int Main(string[] args)
        {
            try
            {
                MediaPool.ProcessArgs(args);
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
            Console.Out.WriteLine("Usage: mediapool.exe [options] <hostname>");
            Console.Out.WriteLine("Gets the info for all the media in the media pool for an ATEM switcher");
            Console.Out.WriteLine();
            Console.Out.WriteLine("Arguments:");
            Console.Out.WriteLine();
            Console.Out.WriteLine(" hostname        - The hostname or IP of the ATEM switcher");
            Console.Out.WriteLine();
            Console.Out.WriteLine("Options:");
            Console.Out.WriteLine();
            Console.Out.WriteLine(" -h, --help      - This help message");
            Console.Out.WriteLine(" -d, --debug     - Debug output");
            Console.Out.WriteLine(" -v, --version   - Version information");
            Console.Out.WriteLine(" -f, --format    - The output format. Either xml, csv, json or text");
            Console.Out.WriteLine();
        }

        private static void ProcessArgs(string[] args)
        {
            IList<string> args1 = new List<string>();
            MediaPool.Format format = MediaPool.Format.Text;
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
                        MediaPool.Help();
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

                    case "-f":
                    case "--format":
                        if (args.Length > index)
                        {
                            switch (args[index + 1].ToLower())
                            {
                                case "json":
                                    format = MediaPool.Format.JSON;
                                    break;
                                case "xml":
                                    format = MediaPool.Format.XML;
                                    break;
                                case "csv":
                                    format = MediaPool.Format.CSV;
                                    break;
                                case "text":
                                    format = MediaPool.Format.Text;
                                    break;
                                default:
                                    throw new SwitcherLibException(String.Format("Unknown format: {0}", format));
                            }
                            index++;
                            break;
                        }
                        break;

                    default:
                        args1.Add(args[index]);
                        break;
                }
            }
            MediaPool.ListMediaPool(format, args1);
        }

        private static void ListMediaPool(MediaPool.Format format, IList<string> args)
        {
            if (args.Count < 1)
            {
                MediaPool.Help();
                throw new SwitcherLibException("Invalid arguments");
            }

            Switcher switcher = new Switcher(args[0]);
            Log.Debug(String.Format("Switcher: {0}", switcher.GetProductName()));
            IList<MediaStill> stills = switcher.GetStills();

            switch (format)
            {
                case MediaPool.Format.Text:
                    foreach (MediaStill still in stills)
                    {
                        Console.Out.WriteLine();
                        Console.Out.WriteLine(String.Format("         Name: {0}", still.Name));
                        Console.Out.WriteLine(String.Format("         Hash: {0}", still.Hash));
                        Console.Out.WriteLine(String.Format("         Slot: {0}", still.Slot.ToString()));
                        Console.Out.WriteLine(String.Format(" Media Player: {0}", still.MediaPlayer.ToString()));
                    }
                    break;

                case MediaPool.Format.JSON:
                    Console.Out.WriteLine(JsonConvert.SerializeObject(stills));
                    break;

                case MediaPool.Format.XML:
                    XmlSerializer xml = new XmlSerializer(stills.GetType());
                    xml.Serialize(Console.Out, stills);
                    break;

                case MediaPool.Format.CSV:
                    foreach (MediaStill still in stills)
                    {
                        Console.Out.WriteLine(still.ToCSV());
                    }
                    break;

                default:
                    Console.Out.WriteLine(stills.ToString());
                    break;
            }
        }
    }
}
