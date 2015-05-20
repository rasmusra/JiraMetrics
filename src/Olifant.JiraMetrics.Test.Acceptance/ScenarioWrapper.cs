using System;
using Olifant.JiraMetrics.Lib;
using Olifant.JiraMetrics.Lib.Metrics;
using Olifant.JiraMetrics.Lib.Metrics.Filters;
using Olifant.JiraMetrics.Test.Acceptance.Pages;
using Olifant.JiraMetrics.Test.Utilities.Fakes;
using OpenQA.Selenium.PhantomJS;
using TechTalk.SpecFlow;

namespace Olifant.JiraMetrics.Test.Acceptance
{
    internal static class ScenarioWrapper
    {
        public static bool ResetDbAfterScenario
        {
            get
            {
                if (!ScenarioContext.Current.ContainsKey("ResetDbAfterScenario"))
                {
                    ResetDbAfterScenario = true;
                }

                return (bool)ScenarioContext.Current["ResetDbAfterScenario"];
            }
            set
            {
                ScenarioContext.Current["ResetDbAfterScenario"] = value;
            }
        }

        public static PageNavigator PageNavigator
        {
            get
            {
                if (!ScenarioContext.Current.ContainsKey("PageNavigator"))
                {
                    PageNavigator = new PageNavigator();
                }

                return (PageNavigator)ScenarioContext.Current["PageNavigator"];
            }
            set
            {
                ScenarioContext.Current["PageNavigator"] = value;
            }
        }
    }
}