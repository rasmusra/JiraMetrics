using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using Olifant.JiraMetrics.Test.Utilities.Helpers;
using OpenQA.Selenium.Support.UI;

namespace Olifant.JiraMetrics.Test.Acceptance.Pages
{
    public class AdminPage 
    {
        public AdminPage(PhantomJsWrapper phantomWrapper)
        {
            PhantomWrapper = phantomWrapper;
        }

        public PhantomJsWrapper PhantomWrapper { get; private set; }

        public void NavigateTo()
        {
            var url = String.Format("http://localhost:{0}/Admin", IisExpressManager.Port);
            PhantomWrapper.Driver.Navigate().GoToUrl(url);
        }

        public void Load(string project)
        {
            FeatureWrapper.JQuery.Find("#projectTextBox")
                .Val(project);

            FeatureWrapper.JQuery.Find("#Load")
                .Click();
        }
    }
}