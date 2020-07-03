using Bromine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bromine.Tests
{
    public class BaseTest
    {
        protected static PageFactory pageFactory
        {
            get { return PageFactory.Instance; }
        }
    }
}
