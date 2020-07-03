using System;

namespace Bromine.Core
{
    public class TypeNotSupportedException : ApplicationException
    {
        public TypeNotSupportedException()
            : base()
        {
        }

        public TypeNotSupportedException(string message) : base(message) { }

        public TypeNotSupportedException(string message, Exception ex) : base(message, ex) { }
    }
}