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
        internal static CycleTimeRule CycleTimeRule
        {
            get
            {
                return (CycleTimeRule)ScenarioContext.Current["CycleTimeRule"];
            }

            set
            {
                ScenarioContext.Current["CycleTimeRule"] = value;
            }
        }

        internal static IIssueFilter DoneDateTimeFilter
        {
            get
            {
                const string Key = "DoneDateFilter";
                return ScenarioContext.Current.ContainsKey(Key) ? (IIssueFilter)ScenarioContext.Current[Key] : null;
            }

            set
            {
                ScenarioContext.Current["DoneDateFilter"] = value;
            }
        }

        internal static FakeTextEditorProxy FakeTextEditorProxy
        {
            get
            {
                return (FakeTextEditorProxy)ScenarioContext.Current["FakeTextEditorProxy"];
            }

            set
            {
                ScenarioContext.Current["FakeTextEditorProxy"] = value;
            }
        }

        internal static JiraMetricsFacade JiraCyclesManager
        {
            get
            {
                return (JiraMetricsFacade)ScenarioContext.Current["JiraCyclesManager"];
            }

            set
            {
                ScenarioContext.Current["JiraCyclesManager"] = value;
            }
        }

        internal static string Jql
        {
            get
            {
                return ScenarioContext.Current.ContainsKey("Jql") ? (string)ScenarioContext.Current["Jql"] : String.Empty;
            }

            set
            {
                ScenarioContext.Current["Jql"] = value;
            }
        }

        internal static bool SaveQuery
        {
            get
            {
                if (!ScenarioContext.Current.ContainsKey("SaveQuery"))
                {
                    SaveQuery = true;
                }

                return (bool)ScenarioContext.Current["SaveQuery"];
            }

            set
            {
                ScenarioContext.Current["SaveQuery"] = value;
            }
        }

        internal static IIssueFilter StartDateTimeFilter
        {
            get
            {
                const string Key = "StartDateFilter";
                return ScenarioContext.Current.ContainsKey(Key) ? (IIssueFilter)ScenarioContext.Current[Key] : new StartDateFilter();
            }

            set
            {
                ScenarioContext.Current["StartDateFilter"] = value;
            }
        }

        internal static IIssueFilter WorkDoneFilter
        {
            get
            {
                const string Key = "WorkDoneFilter";
                return ScenarioContext.Current.ContainsKey(Key) ? (IIssueFilter)ScenarioContext.Current[Key] : null;
            }

            set
            {
                ScenarioContext.Current["WorkDoneFilter"] = value;
            }
        }

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
                    PageNavigator = new PageNavigator(new PhantomJSDriver());
                }

                return (PageNavigator)ScenarioContext.Current["PageNavigator"];
            }
            set
            {
                ScenarioContext.Current["PageNavigator"] = value;
            }
        }

        public static FakeJiraRestClient FakeJiraRestClient
        {
            get
            {
                if (!ScenarioContext.Current.ContainsKey("FakeJiraRestClient"))
                {
                    FakeJiraRestClient = null;
                }

                return (FakeJiraRestClient)ScenarioContext.Current["FakeJiraRestClient"];
            }
            set
            {
                ScenarioContext.Current["FakeJiraRestClient"] = value;
            }
        }
    }
}