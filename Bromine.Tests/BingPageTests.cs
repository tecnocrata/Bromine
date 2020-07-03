using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bromine.Core;
using Bromine.SeleniumCore;

namespace Bromine.Tests
{
    [TestClass]
    public class BingPageTests : BaseTest
    {
        [ClassInitialize()]
        public static void InitializeTests(TestContext context)
        {
            PageFactory.Instance.Register(typeof(SeleniumManager), BrowserType.Chrome);
        }

        [TestMethod]
        public void BingVerifyLoadElementsCorrectly()
        {
            var bingpage = pageFactory.CreatePage<BingPage>();
            bingpage.Navigate("http://www.bing.com/");

            Assert.AreEqual("Bing", bingpage.CurrentPageTitle);
            Assert.IsNotNull(bingpage.SearchText);
            Assert.IsNotNull(bingpage.BtnSubmit);
        }

        [TestMethod]
        public void BingVerifyCorrectNavigationBetweenPages()
        {
            var bingpage = pageFactory.CreatePage<BingPage>();
            bingpage.Navigate("http://www.bing.com/");
            bingpage.SearchText.Text = "Surface 3 Pro";
            bingpage.BtnSubmit.Click();

            var resultpage = bingpage.WaitForPage<ResultPage>();

            Assert.IsNotNull(resultpage.BtnSearchGo);
        }
    }

    public class BingPage : BasePage
    {
       //[FindBy(FindByType.ByXPath, ".//*[@id='sb_form_q']")]
        [FindBy(FindByType.ByName, "q")]
        public ITextBox SearchText
        {
            get { return Controls["SearchText"] as ITextBox; }
        }

        [FindBy(FindByType.ById, "sb_form_go")]
        public IInputButton BtnSubmit
        {
            get { return Controls["BtnSubmit"] as IInputButton; }
        }
    }

    public class ResultPage : BasePage
    {
        [FindBy(FindByType.ById, "sb_form_go")]
        public IButton BtnSearchGo
        {
            get { return Controls["BtnSearchGo"] as IButton; }
        }

        [FindBy(FindByType.ById, "sb_form_q")]
        public override IWebControl VisibilityProofControl
        {
            get { return Controls["VisibilityProofControl"]; }
        }
    }
}
