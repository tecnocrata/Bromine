using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bromine.Core;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.ObjectModel;
using System.Threading;


namespace Bromine.TelerikCore
{
    public class TelerikManager : BaseManager, IAutomationManager
    {
        private Manager driver;
        private Browser activeBrowser;

        public TelerikManager(Bromine.Core.BrowserType browserType = Bromine.Core.BrowserType.Ie)
        {
            // Initialize the settings
            Settings mySettings = new Settings();
            mySettings.Web.DefaultBrowser = ArtOfTest.WebAii.Core.BrowserType.Chrome;
            switch (browserType)
            {
                case Bromine.Core.BrowserType.Firefox:
                    mySettings.Web.DefaultBrowser = ArtOfTest.WebAii.Core.BrowserType.FireFox;
                    break;
                case Bromine.Core.BrowserType.Ie:
                    mySettings.Web.DefaultBrowser = ArtOfTest.WebAii.Core.BrowserType.InternetExplorer;
                    break;
                case Bromine.Core.BrowserType.Chrome:
                    mySettings.Web.DefaultBrowser = ArtOfTest.WebAii.Core.BrowserType.Chrome;
                    break;
                default:
                    mySettings.Web.DefaultBrowser = ArtOfTest.WebAii.Core.BrowserType.FireFox;
                    break;
            }
            driver = new Manager(mySettings);
            // Start the manager
            driver.Start();
            driver.LaunchNewBrowser();
            driver.ActiveBrowser.WaitUntilReady();
            driver.ActiveBrowser.AutoWaitUntilReady = true;
            driver.SetNewBrowserTracking(true);
            activeBrowser = driver.ActiveBrowser;
            var browser = new TelerikBrowser();
            browser.Name = driver.ActiveBrowser.ClientId;
            Windows.Add(browser);
        }

        public Bromine.Core.BrowserType BrowserType { get; set; }

        public IHtmlResult CurrentPage { get; set; }

        public string CurrentPageTitle
        {
            get
            {
                activeBrowser.RefreshDomTree();
                return activeBrowser.PageTitle.Trim();
            }
        }

        public string BrowserId
        {
            get { return activeBrowser.ClientId; }
        }

        public override IHtmlResult NavigateTo(string url)
        {
            activeBrowser.NavigateTo(url);
            activeBrowser.WaitUntilReady();
            activeBrowser.RefreshDomTree();
            CurrentPage = new HtmlResult();
            CurrentPage.HtmlContent = activeBrowser.ViewSourceString;
            return CurrentPage;
        }

        public void Back()
        {
            activeBrowser.GoBack();
            CurrentPage = new HtmlResult();
            activeBrowser.RefreshDomTree();
            CurrentPage.HtmlContent = activeBrowser.ViewSourceString;
        }

        public void CloseCurrentBrowser()
        {
            activeBrowser.Close();
        }
        public void Dispose()
        {
            driver.Dispose();
        }
        public ITextBox FindTextBoxById(string id)
        {
            try
            {
                var txt = activeBrowser.Find.ById(id);
                return new TelerikTextBox(txt);
            }
            catch (Exception ex)
            {
                throw new ElementNotFoundException("No element found id=" + id, ex);
            }
        }

        public IWebControl FindElementBy(FindByType criteria, string textCriteria, Type type)
        {
            IWebControl control = null;
            Element element = null;

            try
            {
                activeBrowser.RefreshDomTree();
                activeBrowser.WaitUntilReady();
                switch (criteria)
                {
                    case FindByType.ById:
                        {
                            element = activeBrowser.Find.ById(textCriteria);
                            break;
                        }
                    case FindByType.ByName:
                        {
                            element = activeBrowser.Find.ByName(textCriteria);
                            break;
                        }
                    case FindByType.ByClassName:
                        {
                            element = activeBrowser.Find.ByAttributes("class=~" + textCriteria);
                            break;
                        }
                    case FindByType.ByCssSelector:
                        {
                            string[] result;
                            string[] stringSeparators = new string[] { "." };
                            result = textCriteria.Split(stringSeparators, StringSplitOptions.None);
                            element = activeBrowser.Find.ByExpression("class=" + result[1], "tagname=" + result[0]);
                            break;
                        }
                    case FindByType.ByXPath:
                        {
                            element = activeBrowser.Find.ByXPath(textCriteria);
                            break;
                        }
                }

                if (element == null)
                    throw new CriteriaNotSupportedException("Criteria " + criteria + " is not supported");

                if (type == typeof(ITextBox)) control = new TelerikTextBox(element);
                else if (type == typeof(IButton)) control = new TelerikButton(element);
                else if (type == typeof(IDropDownList)) control = new TelerikDropDownList(element);
                else if (type == typeof(ILabel)) control = new TelerikLabel(element);
                else if (type == typeof(IInputButton)) control = new TelerikInputButton(element);
                else if (type == typeof(IWebControl)) control = new TelerikBaseControl(element);
                else throw new TypeNotSupportedException("Type " + type.ToString() + " is not supported");

                return control;
            }
            catch (TypeNotSupportedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ElementNotFoundException("No element found by textcriteria =" + textCriteria, ex);
            }
        }

        public IWebControl FindElementBy(FindByType criteria, string textCriteria, Type type, WaitingConditionType waitCondition = WaitingConditionType.NoWait, int timeoutInSeconds = 90)
        {
            activeBrowser.RefreshDomTree();
            if (waitCondition == WaitingConditionType.NoWait)
            {
                return FindElementBy(criteria, textCriteria, type);
            }

            IWebControl control = null;
            Element element = null;

            if (timeoutInSeconds < 1) timeoutInSeconds = 1;

            try
            {
                switch (criteria)
                {
                    case FindByType.ById:
                        {
                            if (waitCondition == WaitingConditionType.UntilElementIsExists)
                            {
                                activeBrowser.Find.ById(textCriteria).Wait.ForExists(timeoutInSeconds * 1000);
                            }
                            else
                            {
                                activeBrowser.Find.ById<HtmlControl>(textCriteria).Wait.ForVisible(timeoutInSeconds * 1000);
                            }
                            element = activeBrowser.Find.ById(textCriteria);
                            break;
                        }
                    case FindByType.ByName:
                        {
                            if (waitCondition == WaitingConditionType.UntilElementIsExists)
                            {
                                activeBrowser.Find.ByName(textCriteria).Wait.ForExists(timeoutInSeconds * 1000);
                            }
                            else
                            {
                                activeBrowser.Find.ByName<HtmlControl>(textCriteria).Wait.ForVisible(timeoutInSeconds * 1000);
                            }
                            element = activeBrowser.Find.ByName(textCriteria);
                            break;
                        }
                    case FindByType.ByClassName:
                        {
                            if (waitCondition == WaitingConditionType.UntilElementIsExists)
                            {
                                activeBrowser.Find.ByAttributes("class=~" + textCriteria).Wait.ForExists(timeoutInSeconds * 1000);
                            }
                            else
                            {
                                activeBrowser.Find.ByAttributes<HtmlControl>("class=~" + textCriteria).Wait.ForVisible(timeoutInSeconds * 1000);
                            }
                            element = activeBrowser.Find.ByAttributes("class=~" + textCriteria);
                            break;
                        }
                    case FindByType.ByCssSelector:
                        {
                            string[] result;
                            string[] stringSeparators = new string[] { "." };
                            result = textCriteria.Split(stringSeparators, StringSplitOptions.None);
                            activeBrowser.Find.ByExpression("class=" + result[1], "tagname=" + result[0]).Wait.ForExists(timeoutInSeconds * 1000);
                            element = activeBrowser.Find.ByExpression("class=" + result[1], "tagname=" + result[0]);
                            break;
                        }
                    case FindByType.ByXPath:
                        {
                            if (waitCondition == WaitingConditionType.UntilElementIsExists)
                            {
                                activeBrowser.Find.ByXPath(textCriteria).Wait.ForExists(timeoutInSeconds * 1000);
                            }
                            else
                            {
                                activeBrowser.Find.ByXPath<HtmlControl>(textCriteria).Wait.ForVisible(timeoutInSeconds * 1000);
                            }

                            element = activeBrowser.Find.ByXPath(textCriteria);
                            break;
                        }
                }

                if (element == null)
                    throw new CriteriaNotSupportedException("Criteria " + criteria + " is not supported");


                if (type == typeof(ITextBox)) control = new TelerikTextBox(element);
                else if (type == typeof(IButton)) control = new TelerikButton(element);
                else if (type == typeof(IDropDownList)) control = new TelerikDropDownList(element);
                else if (type == typeof(IInputButton)) control = new TelerikInputButton(element);
                else if (type == typeof(ILabel)) control = new TelerikLabel(element);
                else if (type == typeof(IWebControl)) control = new TelerikBaseControl(element);
                else throw new TypeNotSupportedException("Type " + type.ToString() + " is not supported");

                return control;
            }
            catch (TypeNotSupportedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ElementNotFoundException("No element found by textcriteria =" + textCriteria, ex);
            }
        }

        public void WaitControl(FindByType criteria, string textCriteria, WaitCondition condition)
        {
            
        }

        protected override IBrowserWindow CreateBrowserWindow()
        {
            var browser = new TelerikBrowser();
            bool ok = false;
            int intialCount = Windows.Count +1;
            // Up to 10 times
            for (int i = 0; i < 90; i++)
            {
                if (intialCount <= driver.Browsers.Count)
                {
                    ok = true;
                    break;
                }
                Thread.Sleep(1000);
            }

            if (!ok)
                throw new TimeOutException("CreateBrowserWindow not equal to  " + intialCount  + " Actual " + driver.Browsers.Count);
            browser.Name = driver.Browsers[Windows.Count].ClientId;
            return browser;
        }

        private void OpenNewBrowser(int height = 0, int width = 0)
        {
            activeBrowser.RefreshDomTree();
            driver.LaunchNewBrowser();
        }
        protected override IBrowserWindow CreateBrowserWindow(int width, int height)
        {
            var browser = new TelerikBrowser();
            int intialCount = driver.Browsers.Count;
            OpenNewBrowser(width, height);
            bool ok = false;

            // Up to 10 times
            for (int i = 0; i < 50; i++)
            {
                if (intialCount + 1 == driver.Browsers.Count)
                {
                    ok = true;
                    break;
                }
                //activeBrowser.Refresh();
                Thread.Sleep(1000);

            }

            if (!ok)
                throw new TimeOutException("CreateBrowserWindow(int width, int height) not equal to  " + (intialCount + 1));
            activeBrowser.WaitUntilReady();
            browser.Name = driver.Browsers[Windows.Count].ClientId;
            return browser;
        }

        protected override void ActivateBrowserWindow(IBrowserWindow window)
        {
            var index = Windows.IndexOf(window);
            driver.Browsers[index].WaitUntilReady();
            driver.Browsers[index].AutoWaitUntilReady = true;
            driver.Browsers[index].Window.Restore();
            driver.Browsers[index].Window.SetFocus();
            driver.Browsers[index].Window.SetActive();

            activeBrowser = driver.Browsers[index];
        }

        public void SetFrameActive(string frameIdOrName)
        {
            throw new NotImplementedException();
        }

        public IWebControl FindElementBy(FindByType criteria, string textCriteria, Type type, WaitCondition waitCondition)
        {
            throw new NotImplementedException();
        }
    }

    public class HtmlResult : IHtmlResult
    {
        public string HtmlContent { get; set; }
    }
}
