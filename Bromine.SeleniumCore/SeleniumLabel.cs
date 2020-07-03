using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bromine.Core;
using OpenQA.Selenium;

namespace Bromine.SeleniumCore
{
    public class SeleniumLabel : ILabel
    {
        private IWebElement element;
        public SeleniumLabel(IWebElement element)
        {
            this.element = element;
        }
        public HtmlContainer Container { get; set; }

        public string Text
        {
            get
            {
                return element.Text;                    
            }
        }

        public bool IsVisible { get { return element.Displayed; } }
    }
}
