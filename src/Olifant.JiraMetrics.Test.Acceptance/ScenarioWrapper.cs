using System;
using Olifant.JiraMetrics.Lib;
using Olifant.JiraMetrics.Lib.Metrics;
using Olifant.JiraMetrics.Lib.Metrics.Filters;
using Olifant.JiraMetrics.Test.Acceptance.Pages;
using Olifant.JiraMetrics.Test.Utilities.Fakes;
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

        public static int PageLoadTimeout
        {
            get
            {
                return (int)ScenarioContext.Current["PageLoadTimeout"];
            }

            set
            {
                ScenarioContext.Current["PageLoadTimeout"] = value;
            }
        }

        public static bool DbNeedstoBeResetAfterScenario
        {
            get
            {
                if (!ScenarioContext.Current.ContainsKey("DbNeedstoBeResetAfterScenario"))
                {
                    DbNeedstoBeResetAfterScenario = true;
                }

                return (bool)ScenarioContext.Current["DbNeedstoBeResetAfterScenario"];
            }
            set
            {
                ScenarioContext.Current["DbNeedstoBeResetAfterScenario"] = value;
            }
        }

        public static BurnUpPage BurnUpPage
        {
            get
            {
                if (!ScenarioContext.Current.ContainsKey("BurnUpPage"))
                {
                    BurnUpPage = null;
                }

                return (BurnUpPage)ScenarioContext.Current["BurnUpPage"];
            }
            set
            {
                ScenarioContext.Current["BurnUpPage"] = value;
            }
        }
    }
}