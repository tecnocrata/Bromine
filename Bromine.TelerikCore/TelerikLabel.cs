using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bromine.Core;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.ObjectModel;


namespace Bromine.TelerikCore
{
    public class TelerikLabel : ILabel
    {
        private Element element;
        public TelerikLabel(Element element)
        {
            this.element = element;
        }
        public HtmlContainer Container { get; set; }

        public string Text
        {
            get
            {
                return element.InnerText;
            }
        }

        public bool IsVisible { get { return (new HtmlInputText(element)).IsVisible(); } }
    }
}
