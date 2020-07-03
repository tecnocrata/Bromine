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
    public class TelerikInputButton : IInputButton
    {
        private Element element;
        public TelerikInputButton(Element element)
        {
            this.element = element;
        }

        public void Click()
        {
            HtmlInputControl button = new HtmlInputControl(element);
            button.Click();
        }

        public bool IsVisible
        {
            get { return (new HtmlInputControl(element)).IsVisible(); }
        }
    }
}
