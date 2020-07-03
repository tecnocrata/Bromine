using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bromine.Core
{
    public class ElementNotFoundException : ApplicationException
    {
        public ElementNotFoundException() : base()
        {
        }

        public ElementNotFoundException(string message) : base(message) { }

        public ElementNotFoundException(string message, Exception ex) : base(message, ex) { }
    }
}
