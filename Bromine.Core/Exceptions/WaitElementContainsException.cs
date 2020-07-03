using System;

namespace Bromine.Core
{
    public class WaitElementContainsException : ApplicationException
    {
        public WaitElementContainsException()
            : base()
        {
        }

        public WaitElementContainsException(string message) : base(message) { }

        public WaitElementContainsException(string message, Exception ex) : base(message, ex) { }
    }
}