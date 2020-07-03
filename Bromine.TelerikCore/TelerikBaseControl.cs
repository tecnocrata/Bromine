using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.ObjectModel;
using Bromine.Core;

namespace Bromine.TelerikCore
{
    public class TelerikBaseControl : IWebControl
    {
        private Element element;
        public TelerikBaseControl(Element element)
        {
            this.element = element;
        }

        public bool IsVisible
        {
            get { return (new HtmlControl(element)).IsVisible(); }
        }
    }
}