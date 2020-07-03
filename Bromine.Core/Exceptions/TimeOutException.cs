using System;

namespace Bromine.Core
{
    public class TimeOutException : ApplicationException
    {
        public TimeOutException()
            : base()
        {
        }

        public TimeOutException(string message) : base(message) { }

        public TimeOutException(string message, Exception ex) : base(message, ex) { }
    }
}