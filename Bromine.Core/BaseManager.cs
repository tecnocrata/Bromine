using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bromine.Core
{
    public abstract class BaseManager
    {
        private List<IBrowserWindow> windows = new List<IBrowserWindow>();
        public List<IBrowserWindow> Windows
        {
            get { return windows; }
        }

        protected BaseManager()
        {
           //TODO: windows.Add(null); Initial window I need to refactor it
        }

        protected abstract IBrowserWindow CreateBrowserWindow();
        protected abstract IBrowserWindow CreateBrowserWindow(int width, int height);
        protected abstract void ActivateBrowserWindow(IBrowserWindow window);
        public abstract IHtmlResult NavigateTo(string url);

        public IBrowserWindow OpenBrowserWindow(bool activatenow = false)
        {
            var browser = CreateBrowserWindow();
            windows.Add(browser);
            if (activatenow) ActivateBrowserWindow(browser);
            return browser;
        }

        public IBrowserWindow OpenBrowserWindow(bool activatenow, string url, int width, int height)
        {
            var browser = CreateBrowserWindow(width, height);
            windows.Add(browser);
            if (activatenow)
            {
                ActivateBrowserWindow(browser);
            }
            NavigateTo(url);
            return browser;
        }

        public void ActivateBrowserWindow(string browserId)
        {
            var browser = (from w in windows
                           where  w.Name == browserId
                select w).FirstOrDefault();
            ActivateBrowserWindow(browser);
        }
    }
}
