using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bromine.Core;
using Bromine.SeleniumCore;

namespace Bromine.Tests
{
    [TestClass]
    public class GooglePageTests : BaseTest
    {
        [ClassInitialize()]
        public static void InitializeTests(TestContext context)
        {
            PageFactory.Instance.Register(typeof(SeleniumManager), BrowserType.Chrome);
        }

        [TestMethod]
        public void GoogleCreateCorrectPage()
        {
            var page = pageFactory.CreatePage<SearchEnginePage>();

            Assert.IsNotNull(page);
        }

        [TestMethod]
        public void GoogleNavigateReturnHtmlContent()
        {
            string url = @"http://www.google.com";
            var page = pageFactory.CreatePage<SearchEnginePage>();

            var content = page.Navigate(url);

            Assert.IsNotNull(content, "Content it is null");
            Assert.IsNotNull(content.HtmlContent, "Html Content it is null");
            Assert.IsTrue(!string.IsNullOrEmpty(content.HtmlContent), "Html Content is empty");
            Assert.IsTrue(content.HtmlContent.Contains("Google"));
        }

        [TestMethod]
        public void GoogleGetTextBoxThruApi()
        {
            string url = @"http://www.google.com";
            var page = pageFactory.CreatePage<SearchEnginePage>();
            page.Navigate(url);
            var element = page.FakeGetInputBoxElement("lst-ib");

            Assert.IsNotNull(element);
            Assert.AreEqual(string.Empty, element.Text);
        }

        [TestMethod]
        [ExpectedException(typeof(ElementNotFoundException), "Expected exception ElementNotFoundException")]
        public void GoogleGetTextBoxThruApiIncorrectId()
        {
            string url = @"http://www.google.com";
            var page = pageFactory.CreatePage<SearchEnginePage>();
            page.Navigate(url);

            var element = page.FakeGetInputBoxElement("searchtext");

            Assert.IsNull(element);
        }

        [TestMethod]
        public void GoogleSetTextOnTextBoxElementByCorrectId()
        {
            string url = @"http://www.bing.com";
            var page = pageFactory.CreatePage<SearchEnginePage>();
            page.Navigate(url);
            var element = page.FakeGetInputBoxElement("sb_form_q");

            element.Text = "Enrique";

            Assert.AreEqual("Enrique", element.Text);
        }

        [TestMethod]
        public void GoogleReadingEmptyTextBox()
        {
            string url = @"http://www.google.com";
            var page = pageFactory.CreatePage<SearchEnginePage>();
            page.Navigate(url);

            Assert.IsNotNull(page.SearchText);
            Assert.AreEqual("", page.SearchText.Text);
        }
    }

    public class SearchEnginePage : BasePage
    {
        [FindBy(FindByType.ById, "lst-ib")]
        public ITextBox SearchText
        {
            get
            {
                return Controls["SearchText"] as ITextBox;
            }
        }

        public ITextBox FakeGetInputBoxElement(string id)
        {
            return this.GetElementBy(FindByType.ById, id, typeof(ITextBox)) as ITextBox;
        }
    }
}
