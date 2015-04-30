using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using Olifant.JiraMetrics.Test.Utilities.Helpers;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;

namespace Olifant.JiraMetrics.Test.Acceptance.Pages
{
    public class AdminPage : PageObject
    {
        public AdminPage(PhantomJSDriver driver) : base(driver)
        {
        }

        protected override string VirtualPath { get { return "/Admin"; }  }

        public void Load(string project)
        {
            JQuery.Find("#projectTextBox")
                .Val(project);

            JQuery.Find("#Load")
                .Click();
        }
    }
}