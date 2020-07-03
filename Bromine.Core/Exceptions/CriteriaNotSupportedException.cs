using System;

namespace Bromine.Core
{
    public class CriteriaNotSupportedException : ApplicationException
    {
        public CriteriaNotSupportedException()
            : base()
        {
        }

        public CriteriaNotSupportedException(string message) : base(message) { }

        public CriteriaNotSupportedException(string message, Exception ex) : base(message, ex) { }
    }
}