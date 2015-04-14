using JQSelenium;
using OpenQA.Selenium.PhantomJS;

using TechTalk.SpecFlow;

namespace Olifant.JiraMetrics.Test.Acceptance
{
    internal static class FeatureWrapper
    {
        internal static PhantomJSDriver PhantomJsDriver
        {
            get
            {
                if (!FeatureContext.Current.ContainsKey("PhantomJsDriver"))
                {
                    PhantomJsDriver = null;
                }

                return (PhantomJSDriver)FeatureContext.Current["PhantomJsDriver"];
            }

            set
            {
                FeatureContext.Current["PhantomJsDriver"] = value;
            }
        }

        internal static JQuery JQuery
        {
            get
            {
                if (!FeatureContext.Current.ContainsKey("JQuery"))
                {
                    JQuery = null;
                }

                return (JQuery)FeatureContext.Current["JQuery"];
            }

            set
            {
                FeatureContext.Current["JQuery"] = value;
            }
        }
    }
}