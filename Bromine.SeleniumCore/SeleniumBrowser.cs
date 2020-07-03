using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bromine.Core;

namespace Bromine.SeleniumCore
{
    public class SeleniumBrowser : IBrowserWindow
    {
        public string Name { get; set; }

        public string IdWindow { get; set; }
    }
}
