﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bromine.Core
{
    public interface ILabel : IWebControl
    {
        HtmlContainer Container { get; set; }
        string Text { get; }
    }
}