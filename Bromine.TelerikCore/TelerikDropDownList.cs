using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bromine.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.Controls.HtmlControls;


namespace Bromine.TelerikCore
{
    public class TelerikDropDownList : IDropDownList
    {
        private Element element;
        public TelerikDropDownList(Element element)
        {
            this.element = element;
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
               (new HtmlSelect(element)).SelectByValue(selectValue);
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
                (new HtmlSelect(element)).SelectByText(text);
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
                (new HtmlSelect(element)).SelectByIndex(index);
            }
        }   
        public bool IsVisible { get { return (new HtmlSelect(element)).IsVisible(); } }
    }
}
