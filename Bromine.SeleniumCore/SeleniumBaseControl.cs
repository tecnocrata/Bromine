using Bromine.Core;
using OpenQA.Selenium;

namespace Bromine.SeleniumCore
{
    public class SeleniumBaseControl : IWebControl
    {
        private IWebElement element;
        public SeleniumBaseControl(IWebElement element)
        {
            this.element = element;
        }

        public bool IsVisible
        {
            get { return element.Displayed; }
        }
    }
}