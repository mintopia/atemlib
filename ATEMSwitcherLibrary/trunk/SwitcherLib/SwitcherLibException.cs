using System;

namespace SwitcherLib
{
    public class SwitcherLibException : Exception
    {
        public SwitcherLibException()
        {
        }

        public SwitcherLibException(string message)
            : base(message)
        {
        }

        public SwitcherLibException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
