using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Olifant.JiraMetrics.Test.Acceptance.Pages;
using Olifant.JiraMetrics.Test.Annotations;
using OpenQA.Selenium.PhantomJS;

namespace Olifant.JiraMetrics.Test.Acceptance
{
    public class PhantomJsWrapper
    {
        private readonly PhantomJSDriver _phantomJsDriver;

        public PhantomJsWrapper(PhantomJSDriver phantomJsDriver): this(phantomJsDriver,new TimeSpan()) { }

        public PhantomJsWrapper(PhantomJSDriver phantomJsDriver, TimeSpan loadTimeOut)
        {
            _phantomJsDriver = phantomJsDriver;
            LoadTimeout = TimeSpan.FromSeconds(0);
        }

        [NotNull]
        public TimeSpan LoadTimeout { get; set; }

        public PhantomJSDriver Driver { get { return _phantomJsDriver; } }

        public bool WaitForRendering(Func<bool> shouldBeTrue)
        {
            var found = shouldBeTrue(); 
            var startTime = DateTime.Now;

            while (!found && DateTime.Now - startTime < LoadTimeout)
            {
                found = shouldBeTrue();
                Thread.Sleep(500);
            }

            return found;
        }
    }
}