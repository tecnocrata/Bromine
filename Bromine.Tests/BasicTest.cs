using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bromine.Core;
using Bromine.SeleniumCore;

namespace Bromine.Tests
{
    [TestClass]
    public class BasicTest: BaseTest
    {
        [ClassInitialize()]
        public static void InitializeTests(TestContext context)
        {
            PageFactory.Instance.Register(typeof(SeleniumManager), BrowserType.Chrome);
        }

        [TestMethod]
        public void BasicVerifyCorrectPageDefinition()
        {
            var badpage = pageFactory.CreatePage<BasicBingPage>();
            badpage.Navigate("http://www.bing.com/");

            Assert.IsNotNull(badpage.SearchText);
        }

        [TestMethod]
        [ExpectedException(typeof(TypeNotSupportedException))]
        public void BasicVerifyTypeNotSupportedInPageDefinition()
        {
            var badpage = pageFactory.CreatePage<BadPage>();
            badpage.Navigate("http://www.bing.com/");

            Assert.IsNotNull(badpage.SearchText);
        }

    }

    public class BasicBingPage : BasePage
    {
        [FindBy(FindByType.ById, "sb_form_q")]
        public ITextBox SearchText
        {
            get
            {
                return Controls["SearchText"] as ITextBox; ;
            }
        }
    }
    public class BadPage : BasePage
    {
        [FindBy(FindByType.ByName, "q")]
        public string SearchText
        {
            get
            {
                return Controls["SearchText"].ToString();
            }
        }
    }
}
