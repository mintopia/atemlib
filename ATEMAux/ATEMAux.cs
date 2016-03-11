using SwitcherLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ATEMAux
{
    class ATEMAux
    {
        static int Main(string[] args)
        {
            try
            {
                ATEMAux.ProcessArgs(args);
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
            Console.Out.WriteLine("Usage: atemaux.exe [options] <hostname>");
            Console.Out.WriteLine("Gets the source info for all AUX's of an ATEM switcher");
            Console.Out.WriteLine("optionally set the source for the AUX of an ATEM switcher");
            Console.Out.WriteLine();
            Console.Out.WriteLine("Arguments:");
            Console.Out.WriteLine();
            Console.Out.WriteLine(" hostname        - The hostname or IP of the ATEM switcher");
            Console.Out.WriteLine();
            Console.Out.WriteLine("Options:");
            Console.Out.WriteLine();
            Console.Out.WriteLine(" -h, --help      - This help message");
            Console.Out.WriteLine(" -v, --version   - Version information");
            Console.Out.WriteLine(" -a, --aux    - Aux port number followed by input #");

            Console.Out.WriteLine();
        }
        private static void ProcessArgs(string[] args)
        {
            IList<string> args1 = new List<string>();
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
                        ATEMAux.Help();
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

                    case "-a":
                    case "--aux":
                        if (args.Length > index)
                        {
                            int aux;
                            int source;
                            if (Int32.TryParse(args[index + 1].ToLower(), out aux))
                                Console.WriteLine(String.Format("Set Aux: {0}", aux));
                            else
                                throw new SwitcherLibException(String.Format("No Aux number specified"));

                            if (Int32.TryParse(args[index + 2].ToLower(), out source))
                                Console.WriteLine(String.Format("Set Source: {0}", source));
                            else
                                throw new SwitcherLibException(String.Format("No Aux input specified"));

                            SetAux(args1, aux, source);
                            break;
                        }
                        break;

                    default:
                        ListAux(args1);
                        break;
                }
            }
            
        }
        private static void ListAux(IList<string> args)
        {
            if (args.Count < 1)
            {
                ATEMAux.Help();
                throw new SwitcherLibException("Invalid arguments");
            }

            Switcher switcher = new Switcher(args[0]);
            Log.Debug(String.Format("Switcher: {0}", switcher.GetProductName()));
            IList<SwitcherAuxInput> inputs = switcher.GetAuxInputs();


                    foreach (SwitcherAuxInput input in inputs)
                    {
                        Console.Out.WriteLine();
                        Console.Out.WriteLine(String.Format("   Name: {0}", input.Name));
                        Console.Out.WriteLine(String.Format("     ID: {0}", input.ID.ToString()));
                        Console.Out.WriteLine(String.Format("  Label: {0}", input.Label));
                        Console.Out.WriteLine(String.Format(" Source: {0}", input.Source));
                    }
        }
        private static void SetAux(IList<string> args, int aux, int source)
        {
            Switcher switcher = new Switcher(args[0]);
            Log.Debug(String.Format("Switcher: {0}", switcher.GetProductName()));
            switcher.SetAuxUnput((long)aux, (long)source);
        }
    }


}
