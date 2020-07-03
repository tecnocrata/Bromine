using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bromine.Core;
using Bromine.SeleniumCore;

namespace Bromine.Tests
{
    [TestClass]
    public class GmailPageTests : BaseTest
    {
        [ClassInitialize()]
        public static void InitializeTests(TestContext context)
        {
            PageFactory.Instance.Register(typeof(SeleniumManager), BrowserType.Chrome);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var main = pageFactory.CreatePage<GmailLoginPage>();
            main.Navigate("http://gmail.com");
            main.UserNameText.Text = "autotest552@gmail.com";
            main.NextButton.Click();
            var passwd = main.WaitForPage<PasswordPage>();
            passwd.PasswdText.Text = "auto.test";
            passwd.SignInButton.Click();
            var inbox = passwd.WaitForPage<GmailInboxPage>();
            inbox.SearchText.Text = "Nuevo inicio";
            inbox.SearchButton.Click();
            var link = inbox.GetElementBy(FindByType.ByXPath, @"//div[5]/div/div/table/tbody/tr[1]", typeof(ILink), WaitingConditionType.UntilElementIsExists) as ILink;
            link.Click();
        }
    }

    public class GmailLoginPage : BasePage
    {
        [FindBy(FindByType.ById, "next", WaitingConditionType.UntilElementIsVisible)]
        public override IWebControl VisibilityProofControl
        {
            get { return Controls["VisibilityProofControl"]; }
        }

        [FindBy(FindByType.ById, "Email")]
        public ITextBox UserNameText
        {
            get
            {
                return Controls["UserNameText"] as ITextBox;
            }
        }

        [FindBy(FindByType.ById, "next")]
        public IButton NextButton
        {
            get
            {
                return Controls["NextButton"] as IButton;
            }
        }
    }

    public class PasswordPage : BasePage
    {
        [FindBy(FindByType.ById, "signIn", WaitingConditionType.UntilElementIsVisible)]
        public override IWebControl VisibilityProofControl
        {
            get { return Controls["VisibilityProofControl"]; }
        }

        [FindBy(FindByType.ById, "Passwd")]
        public ITextBox PasswdText
        {
            get
            {
                return Controls["PasswdText"] as ITextBox;
            }
        }

        [FindBy(FindByType.ById, "signIn")]
        public IButton SignInButton
        {
            get
            {
                return Controls["SignInButton"] as IButton;
            }
        }
    }

    public class GmailInboxPage : BasePage
    {
        [FindBy(FindByType.ByXPath, @"//div[contains(.,'Principal')]", WaitingConditionType.UntilElementIsVisible, Timeout = 40000)]
        public override IWebControl VisibilityProofControl
        {
            get { return Controls["VisibilityProofControl"]; }
        }

        [FindBy(FindByType.ById, "gbqfq")]
        public ITextBox SearchText
        {
            get
            {
                return Controls["SearchText"] as ITextBox;
            }
        }

        [FindBy(FindByType.ById, "gbqfb")]
        public IButton SearchButton
        {
            get
            {
                return Controls["SearchButton"] as IButton;
            }
        }
    }

}
