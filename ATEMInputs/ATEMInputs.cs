using SwitcherLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ATEMInputs
{
    class ATEMInputs
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
                ATEMInputs.ProcessArgs(args);
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
            Console.Out.WriteLine("Usage: ateminputs.exe [options] <hostname>");
            Console.Out.WriteLine("Gets the info for all inputs for an ATEM switcher");
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
            Console.Out.WriteLine(" -f, --format    - The output format. Only text");
            Console.Out.WriteLine();
        }

        private static void ProcessArgs(string[] args)
        {
            IList<string> args1 = new List<string>();
            ATEMInputs.Format format = ATEMInputs.Format.Text;
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
                        ATEMInputs.Help();
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
                                    format = ATEMInputs.Format.JSON;
                                    break;
                                case "xml":
                                    format = ATEMInputs.Format.XML;
                                    break;
                                case "csv":
                                    format = ATEMInputs.Format.CSV;
                                    break;
                                case "text":
                                    format = ATEMInputs.Format.Text;
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
            ATEMInputs.ListInputs(format, args1);
        }

        private static void ListInputs(ATEMInputs.Format format, IList<string> args)
        {
            if (args.Count < 1)
            {
                ATEMInputs.Help();
                throw new SwitcherLibException("Invalid arguments");
            }

            Switcher switcher = new Switcher(args[0]);
            Log.Debug(String.Format("Switcher: {0}", switcher.GetProductName()));
            IList<SwitcherInput> inputs = switcher.GetInputs();

            switch (format)
            {
                case ATEMInputs.Format.Text:
                    foreach (SwitcherInput input in inputs)
                    {
                        Console.Out.WriteLine();
                        Console.Out.WriteLine(String.Format("       Name: {0}", input.Name));
                        Console.Out.WriteLine(String.Format("         ID: {0}", input.ID.ToString()));
                        Console.Out.WriteLine(String.Format("      Label: {0}", input.Label));
                        Console.Out.WriteLine(String.Format(" Input Type: {0}", input.PortType));
                    }
                    break;

                default:
                    Console.Out.WriteLine(inputs.ToString());
                    break;
            }
        }
    }
}