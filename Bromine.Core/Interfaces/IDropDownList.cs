using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bromine.Core
{
    public interface IDropDownList : IWebControl
    {
        HtmlContainer Container { get; set; }
        string Value { get; set; }
        string Text { get; set; }
        int Index { get; set; }
    }
}
