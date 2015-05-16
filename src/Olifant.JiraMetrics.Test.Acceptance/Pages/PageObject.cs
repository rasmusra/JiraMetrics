using System;
using System.Threading;
using JQSelenium;
using Olifant.JiraMetrics.Test.Utilities.Helpers;
using OpenQA.Selenium.PhantomJS;

namespace Olifant.JiraMetrics.Test.Acceptance.Pages
{
    public abstract class PageObject 
    {
        protected PageObject(PhantomJSDriver driver)
        {
            Driver = driver;
            JQuery = new JQuery(driver);
            LoadTimeout = TimeSpan.FromSeconds(1); // default

            NavigateTo();
        }

        protected PhantomJSDriver Driver { get; private set; }

        protected JQuery JQuery { get; set; }

        public TimeSpan LoadTimeout { get; set; }

        protected abstract string VirtualPath { get; }

        public void Refresh()
        {
            Driver.Navigate().Refresh();
        }

        private void NavigateTo()
        {
            var url = String.Format("http://localhost:{0}{1}", IisExpressManager.Port, VirtualPath);
            Driver.Navigate().GoToUrl(url);
        }

        protected bool TryUntilTimeout(Func<bool> evaluate)
        {
            var startTime = DateTime.Now;
            Func<bool> isTimedOut =  () => DateTime.Now - startTime > LoadTimeout;
            var isOk = evaluate();

            while (!isOk && !isTimedOut())
            {
                isOk = evaluate();
                if (isOk) break;
                Thread.Sleep(500);
            }

            return isOk;
        }
    }
}