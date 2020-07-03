using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bromine.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System.Configuration;

namespace Bromine.SeleniumCore
{
    public class SeleniumManager : BaseManager, IAutomationManager
    {
        private RemoteWebDriver driver;

        public SeleniumManager(BrowserType browserType = BrowserType.Ie)
        {

            switch (browserType)
            {
                case BrowserType.Firefox:
                    driver = new FirefoxDriver();
                    break;
                case BrowserType.Ie:
                    var options = new InternetExplorerOptions
                    {
                        IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                        IgnoreZoomLevel = true
                    };
                    driver = new InternetExplorerDriver(options);
                    break;
                case BrowserType.Chrome:
                    driver = new ChromeDriver(ConfigurationManager.AppSettings["ChromeDriverLocation"]); 
                    break;
                default:
                    driver = new FirefoxDriver();
                    break;
            }
           // driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(10000));
            var browser = new SeleniumBrowser();
            browser.Name = driver.CurrentWindowHandle;
            Windows.Add(browser);
        }
            
        public BrowserType BrowserType { get; set; }

        public IHtmlResult CurrentPage { get; set; }

        public string CurrentPageTitle
        {
            get { return driver.Title.Trim(); }
        }

        public string BrowserId
        {
            get { return driver.CurrentWindowHandle; }
        }

        public override IHtmlResult NavigateTo (string url)
        {
            driver.Navigate().GoToUrl(url);
            CurrentPage = new HtmlResult();
            CurrentPage.HtmlContent = driver.PageSource;
            return CurrentPage;
        }

        public void Back()
        {
            driver.Navigate().Back();
            CurrentPage = new HtmlResult();
            CurrentPage.HtmlContent = driver.PageSource;
        }

        public void CloseCurrentBrowser()
        {
            driver.Close();
        }

        public void Dispose()
        {
            driver.Dispose();
        }

        public IWebControl FindElementBy(FindByType criteria, string textCriteria, Type type)
        {
            IWebControl control = null;
            IWebElement element = null;

            try
            {
                element = GetWebElement(criteria, textCriteria);
                control = GetWebControl(criteria, type, element);
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

        public IWebControl FindElementBy(FindByType criteria, string textCriteria, Type type, WaitingConditionType waitCondition, int timeoutInSeconds = 90)
        {
            if (waitCondition == WaitingConditionType.NoWait)
            {
                return FindElementBy(criteria, textCriteria, type);
            }

            IWebControl control = null;
            IWebElement element = null;

            try
            {
                element = GetWebElement(criteria, textCriteria, waitCondition, timeoutInSeconds);
                control = GetWebControl(criteria, type, element);

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

        public IWebControl FindElementBy(FindByType criteria, string textCriteria, Type type, WaitCondition waitCondition)
        {
            throw new NotImplementedException();
        }

        public void WaitControl(FindByType criteria, string textCriteria, WaitCondition condition)
        {
            var element = GetWebElement(criteria, textCriteria, condition.WaitingConditionType, condition.Timeout);
            //control = GetWebControl(criteria, type, element);
            //return element;
        }

        private static IWebControl GetWebControl(FindByType criteria, Type type, IWebElement element)
        {
            IWebControl control;
            if (element == null)
                throw new CriteriaNotSupportedException("Criteria " + criteria + " is not supported");

            if (type == typeof(ITextBox)) control = new SeleniumTextBox(element);
            else if (type == typeof(IButton)) control = new SeleniumButton(element);
            else if (type == typeof(IDropDownList)) control = new SeleniumDropDownList(element);
            else if (type == typeof(IInputButton)) control = new SeleniumInputButton(element);
            else if (type == typeof(ILabel)) control = new SeleniumLabel(element);
            else if (type == typeof(ILink)) control = new SeleniumLink(element);
            else if (type == typeof(IWebControl)) control = new SeleniumBaseControl(element);
            else throw new TypeNotSupportedException("Type " + type.ToString() + " is not supported");
            return control;
        }

        private IWebElement GetWebElement(FindByType criteria, string textCriteria)
        {
            IWebElement element = null;
            switch (criteria)
            {
                case FindByType.ById:
                    {
                        element = driver.FindElementById(textCriteria);
                        break;
                    }
                case FindByType.ByName:
                    {
                        element = driver.FindElementByName(textCriteria);
                        break;
                    }
                case FindByType.ByClassName:
                    {
                        element = driver.FindElementByClassName(textCriteria);
                        break;
                    }
                case FindByType.ByCssSelector:
                    {
                        element = driver.FindElementByCssSelector(textCriteria);
                        break;
                    }
                case FindByType.ByXPath:
                    {
                        element = driver.FindElementByXPath(textCriteria);
                        break;
                    }
            }
            return element;
        }

        private IWebElement GetWebElement(FindByType criteria, string textCriteria, WaitingConditionType waitCondition,
            int timeoutInSeconds)
        {
            IWebElement element=null;
            if (timeoutInSeconds < 1) timeoutInSeconds = 1;

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

            switch (criteria)
            {
                case FindByType.ById:
                {
                    element = GetElement(wait, waitCondition, By.Id(textCriteria));
                    break;
                }
                case FindByType.ByName:
                {
                    element = GetElement(wait, waitCondition, By.Name(textCriteria));
                    break;
                }
                case FindByType.ByClassName:
                {
                    element = GetElement(wait, waitCondition, By.ClassName(textCriteria));
                    break;
                }
                case FindByType.ByCssSelector:
                {
                    element = GetElement(wait, waitCondition, By.CssSelector(textCriteria));
                    break;
                }
                case FindByType.ByXPath:
                {
                    element = GetElement(wait, waitCondition, By.XPath(textCriteria));
                    break;
                }
            }
            return element;
        }

        private IWebElement GetElement(WebDriverWait wait, WaitingConditionType waitCondition, By by)
        {
            IWebElement element = null;

            if (waitCondition == WaitingConditionType.UntilElementIsExists)
            {
                element = wait.Until(ExpectedConditions.ElementExists(by));
            }
            else
            {
                element = wait.Until(ExpectedConditions.ElementIsVisible(by));
            }

            return element;
        }

        protected override IBrowserWindow CreateBrowserWindow()
        {
            var browser = new SeleniumBrowser();
            new WebDriverWait(driver, TimeSpan.FromSeconds(90)).Until<bool>((w) => w.WindowHandles.Count == Windows.Count + 1);
            browser.Name = driver.WindowHandles[Windows.Count];
            return browser;
        }

        private void OpenNewBrowser(int height = 0, int width = 0)
        {
            IJavaScriptExecutor jscript = driver;

            if (height == 0 || width == 0)
            {
                // This wild open a new Tab
                jscript.ExecuteScript("window.open()");
            }
            else
            {
                // This will open a new Windows
                jscript.ExecuteScript(String.Format("window.open('', '', 'height={0}, width={1}')", height, width));
            }
        }

        protected override IBrowserWindow CreateBrowserWindow(int width, int height)
        {
            var browser = new SeleniumBrowser();
            OpenNewBrowser(width, height);
            new WebDriverWait(driver, TimeSpan.FromSeconds(90)).Until<bool>((w) => w.WindowHandles.Count == Windows.Count + 1);
            browser.Name = driver.WindowHandles[Windows.Count];
            return browser;
        }

        protected override void ActivateBrowserWindow(IBrowserWindow window)
        {
            var index = Windows.IndexOf(window);
            string browserName = driver.WindowHandles[index];
            driver.SwitchTo().Window(browserName);
        }
    }

    public class HtmlResult : IHtmlResult
    {
        public string HtmlContent { get; set; }
    }
}