using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bromine.Core;
using OpenQA.Selenium;

namespace Bromine.SeleniumCore
{
    public class SeleniumButton : IButton
    {
        private IWebElement element;
        public SeleniumButton(IWebElement element)
        {
            this.element = element;
        }

        public void Click()
        {
            element.Click();
        }

        public bool IsVisible
        {
            get { return element.Displayed; }
        }
    }
}
