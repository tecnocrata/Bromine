using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Practices.Unity;

namespace Bromine.Core
{
    public class BasePage
    {
        protected ControlDictionary Controls;
        public String BrowserID;

        public BasePage()
        {
            Controls = new ControlDictionary(this);
            if (Manager != null)
                BrowserID = Manager.BrowserId;
        }

        [Dependency]
        protected IAutomationManager Manager { get; set; }

        public virtual IWebControl VisibilityProofControl { get { return null; } }

        public string CurrentPageTitle
        {
            get { return Manager.CurrentPageTitle; }
        }

        public IWebControl GetElementBy(FindByType criteria, string textCriteria, Type type, WaitingConditionType waitCondition = WaitingConditionType.NoWait)
        {
            return Manager.FindElementBy(criteria, textCriteria, type, waitCondition);
        }

        public virtual T WaitForPage<T>() where T : BasePage
        {
            var page = PageFactory.Instance.CreatePage<T>();
            page.BrowserID = Manager.BrowserId;
            if (!ProofVisibleContent(page))
                throw new VisibilityProofElementNotFoundException(
                    "It was not possible to wait more time for the page " + typeof(T).Name);
            return page;
        }

        public virtual T WaitForPopup<T>() where T : BasePage
        {
            Manager.OpenBrowserWindow(activatenow: true);
            return WaitForPage<T>();
        }

        public virtual T WaitForPopup<T>(string url, int width, int height) where T : BasePage
        {
            Manager.OpenBrowserWindow(true, url, width, height);
            return WaitForPage<T>();
        }

        public T WaitForControl<T>(Expression<Func<T>> x, WaitCondition condition) where T : IWebControl
        {
            var property = ((PropertyInfo)((MemberExpression)x.Body).Member);
            var attrib = GetAttribute(property);
            Manager.WaitControl(attrib.Criteria, attrib.TextCriteria, condition);
            return default(T);
        }

        private FindByAttribute GetAttribute(PropertyInfo property)
        {
            var attrs = property.GetCustomAttributes(true);
            var a = attrs.OfType<FindByAttribute>().FirstOrDefault();
            return a;
        }

        protected bool ProofVisibleContent(BasePage page)
        {
            try
            {

                // Up to 10 times
                for (int i = 0; i < 30; i++)
                {
                    try
                    {
                        // Check whether our element is visible yet
                        if (page.VisibilityProofControl.IsVisible)
                        {
                            return true;
                        }

                    }
                    catch (Exception e) { }
                    finally
                    {
                        Thread.Sleep(1000);
                    }
                }
                if (page.VisibilityProofControl == null)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new VisibilityProofElementNotFoundException("It was not possible to find an element that proof the page load", ex);
            }
            return false;
        }

        public void ActivateBrowserWindow()
        {
            Manager.ActivateBrowserWindow(BrowserID);
        }

        public void CloseBrowserWindow()
        {
            Manager.CloseCurrentBrowser();
        }

        public IHtmlResult Navigate(string url)
        {
            BrowserID = Manager.BrowserId;
            return Manager.NavigateTo(url);
        }
    }
}
