using System;
using System.Configuration;
using JQSelenium;
using OpenQA.Selenium.PhantomJS;

namespace Olifant.JiraMetrics.Test.Acceptance.Pages
{
    internal class PageNavigator : IDisposable
    {
        private readonly PhantomJSDriver _driver;

        public PageNavigator(PhantomJSDriver driver)
        {
            _driver = CreateDriver();
        }

        public PageObject Current { get; private set; }

        public T GetCurrent<T>() where T : PageObject
        {
            return (T) Current;
        }

        public void GoTo(string page)
        {
            Current = CreatePage(page);
        }

        private PageObject CreatePage(string page)
        {
            switch (page.ToLower())
            {
                case "burn-up":
                    return (PageObject)Activator.CreateInstance(typeof(BurnUpPage), _driver);

                case "admin":
                    return (PageObject)Activator.CreateInstance(typeof(AdminPage), _driver);

                default:
                    throw new NotImplementedException("Page = " + page);
            }
        }

        private PhantomJSDriver CreateDriver()
        {
            var phantomDir = ConfigurationManager.AppSettings["PhantomJsDirectory"];
            var driver = new PhantomJSDriver(phantomDir, new PhantomJSOptions(), TimeSpan.FromSeconds(60));
            return driver;
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}