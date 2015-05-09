﻿using System;
using System.Configuration;
using Olifant.JiraMetrics.Test.Utilities.Helpers;
using TechTalk.SpecFlow;

namespace Olifant.JiraMetrics.Test.Acceptance
{
    [Binding]
    public class Hooks
    {
        [BeforeFeature("web")]
        public static void SetUpIis()
        {
            Console.WriteLine("BeforeTestRun: Kill running processes...");
            IisExpressManager.Kill();
            WinProcessWrapper.KillByName("phantomjs");

            Console.WriteLine("BeforeTestRun: Setup db...");
            MongoWrapper.Init(ConfigurationManager.AppSettings["ConnectionString"],
                IssueStubFactory.CreateFromFiles());

            Console.WriteLine("BeforeTestRun: Setup web...");
            IisExpressManager.SetupFakes("FakeStructureMap.xml");

            Console.WriteLine("BeforeTestRun: Starting iisexpress...");
            IisExpressManager.Start();
        }

        [AfterFeature("web")]
        public static void TearDownIis()
        {
            Console.WriteLine("AfterTestRun: Tear down web...");
            IisExpressManager.RemoveFakes();
            IisExpressManager.Kill();
        }

        [AfterScenario("web")]
        public static void TearDownWebDriver()
        {
            try
            {
                ScenarioWrapper.PageNavigator.Dispose();
                WinProcessWrapper.KillByName("phantomjs");
            }
            catch (Exception e)
            {
                Console.Write("Problems when tearing down web driver: " + e);
            }
        }

        [BeforeFeature]
        public static void MakeFeatureLogReadableBefore()
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("Feature: " + FeatureContext.Current.FeatureInfo.Title);
            Console.WriteLine(FeatureContext.Current.FeatureInfo.Description);
            Console.WriteLine();
        }

        [BeforeScenario]
        public static void MakeScenarioLogReadableBefore()
        {
            Console.WriteLine("Scenario: " + ScenarioContext.Current.ScenarioInfo.Title);
        }

        [AfterScenario]
        public static void MakeScenarioLogReadableAfter()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// Add this tag to skip db-reset after scenario
        /// </summary>
        [BeforeScenario("no_data_changes")]
        public static void RememberThatDbWillNotChange()
        {
            ScenarioWrapper.ResetDbAfterScenario = false;
        }

        [AfterScenario]
        public static void ResetChangedDataAfterScenario()
        {
            if (ScenarioWrapper.ResetDbAfterScenario)
            {
                MongoWrapper.Instance.InitTestPopulation(IssueStubFactory.CreateFromFiles());
            }

            ScenarioWrapper.ResetDbAfterScenario = true;
        }
    }
}
