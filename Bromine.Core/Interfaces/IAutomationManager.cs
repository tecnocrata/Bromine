using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bromine.Core
{
    public interface IAutomationManager : IDisposable
    {
        BrowserType BrowserType { get; }
        IHtmlResult CurrentPage { get; }
        string CurrentPageTitle { get; }
        string BrowserId { get; }
        IHtmlResult NavigateTo(string url);
        IBrowserWindow OpenBrowserWindow(bool activatenow);
        IBrowserWindow OpenBrowserWindow(bool activatenow, string url, int width, int height);
        void ActivateBrowserWindow(string browserTitleName);
        void Back();
        void CloseCurrentBrowser();
        IWebControl FindElementBy(FindByType criteria, string textCriteria, Type type, WaitingConditionType waitCondition = WaitingConditionType.NoWait, int timeoutInSeconds = 90);
        IWebControl FindElementBy(FindByType criteria, string textCriteria, Type type, WaitCondition waitCondition);
        void WaitControl(FindByType criteria, string textCriteria, WaitCondition condition);
    }

    public enum BrowserType
    {
        Ie = 1,
        Firefox = 2,
        Chrome = 3,
        PhantomJs = 4,
    }

    public enum AutomationEngine
    {
        Telerik = 1,
        Selenium = 2,
        CodeUI = 3
    }

    public interface IHtmlQuery
    {
        IHtmlResult FindElementById(string id);
    }

    public interface IHtmlResult
    {
        string HtmlContent { get; set; }
    }
}
