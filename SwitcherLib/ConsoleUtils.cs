using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherLib
{
    public class ConsoleUtils
    {
        public static void Version()
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            AssemblyTitleAttribute title = (AssemblyTitleAttribute)Assembly.GetEntryAssembly().GetCustomAttribute(typeof(AssemblyTitleAttribute));
            Console.Out.WriteLine(String.Format("{0} {1}.{2}.{3}", title.Title, version.Major.ToString(), version.Minor.ToString(), version.Revision.ToString()));
            Console.Out.WriteLine("Michael Smith <me@murray-mint.co.uk> and Ian Morrish <ian_morrish@hotmail.com>");
            Console.Out.WriteLine("This software is released under the MIT License");
        }
    }
}
