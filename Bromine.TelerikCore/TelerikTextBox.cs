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
    public class TelerikTextBox : ITextBox
    {
        private Element element;
        public TelerikTextBox(Element element)
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
                (new HtmlInputControl(element)).Value = text;
            }
        }

        public bool IsVisible { get { return (new HtmlInputText(element)).IsVisible(); } }
    }
}
