using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bromine.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Bromine.SeleniumCore
{
    public class SeleniumDropDownList : IDropDownList
    {
        private IWebElement element;
        private SelectElement selectorItem;
        public SeleniumDropDownList(IWebElement element)
        {
            this.element = element;
            selectorItem = new SelectElement(element);
        }
        public HtmlContainer Container { get; set; }

        private string selectValue=string.Empty;
        private string text = string.Empty;
        int index = -1;
        public string Value
        {
            get
            {
                return selectValue;
            }
            set
            {
                selectValue = value;
                selectorItem.SelectByValue(selectValue);
            }
        }

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                selectorItem.SelectByText(text);                
            }
        }

        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
                selectorItem.SelectByIndex(index);
            }
        }   
        public bool IsVisible { get { return element.Displayed; } }
    }
}
