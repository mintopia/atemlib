using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherLib
{
    public class Log
    {
        public enum Level
        {
            Debug,
            Info,
            Notice,
            Warning,
            Error,
        }

        public static Log.Level CurrentLevel = Log.Level.Info;

        public static void Debug(string message)
        {
            Log.LogMessage(Log.Level.Debug, message);
        }

        public static void Info(string message)
        {
            Log.LogMessage(Log.Level.Info, message);
        }

        public static void Notice(string message)
        {
            Log.LogMessage(Log.Level.Notice, message);
        }

        public static void Warning(string message)
        {
            Log.LogMessage(Log.Level.Warning, message);
        }

        public static void Error(string message)
        {
            Log.LogMessage(Log.Level.Error, message);
        }

        protected static void LogMessage(Log.Level level, string message)
        {
            if (level < Log.CurrentLevel)
            {
                return;
            }
            Console.Out.WriteLine(message);
        }
    }
}
