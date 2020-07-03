using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bromine.Core
{
    public interface ITextBox : IWebControl
    {
        HtmlContainer Container { get; set; }
        string Text { get; set; }
    }
}
