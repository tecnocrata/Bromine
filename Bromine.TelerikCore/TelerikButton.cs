using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bromine.Core;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.ObjectModel;

namespace Bromine.TelerikCore
{
    public class TelerikButton : IButton
    {
        private Element element;
        public TelerikButton(Element element)
        {
            this.element = element;
        }

        public void Click()
        {
            HtmlContainerControl button = new HtmlContainerControl(element);            
            button.Click();
        }

        public bool IsVisible
        {
            get { return (new HtmlContainerControl(element)).IsVisible(); }
        }
    }
}
