using System;

namespace Bromine.Core
{
    public class VisibilityProofElementNotFoundException : ApplicationException
    {
        public VisibilityProofElementNotFoundException()
            : base()
        {
        }

        public VisibilityProofElementNotFoundException(string message) : base(message) { }

        public VisibilityProofElementNotFoundException(string message, Exception ex) : base(message, ex) { }
    }
}