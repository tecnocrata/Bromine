using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bromine.Core;
using OpenQA.Selenium;

namespace Bromine.SeleniumCore
{
    public class SeleniumTextBox : ITextBox
    {
        private IWebElement element;
        public SeleniumTextBox(IWebElement element)
        {
            this.element = element;
        }
        public HtmlContainer Container { get; set; }

        private string text=string.Empty;

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                element.Clear();
                element.SendKeys(text);
            }
        }

        public bool IsVisible { get { return element.Displayed; } }
    }
}
