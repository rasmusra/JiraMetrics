using OpenQA.Selenium.PhantomJS;

using TechTalk.SpecFlow;

namespace HM.JiraMetrics.Test.Acceptance
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
    }
}