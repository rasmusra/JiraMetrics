using System;
using System.Configuration;
using JQSelenium;
using Olifant.JiraMetrics.Test.Utilities.Helpers;
using OpenQA.Selenium.PhantomJS;
using Olifant.JiraMetrics.Lib.Jira.Model;
using TechTalk.SpecFlow;

namespace Olifant.JiraMetrics.Test.Acceptance
{
    [Binding]
    public class Hooks
    {
        [BeforeTestRun]
        public static void SetupStubsAndFakes()
        {
            WebServer.SetupFakes("FakeStructureMap.xml");
            MongoWrapper.Init(ConfigurationManager.AppSettings["ConnectionString"]);
            MongoWrapper.Instance.Reset();
            var issues = IssueStubFactory.CreateFromFiles();
            MongoWrapper.Instance.GetCollection<Issue>().InsertBatch(issues);
        }

        [AfterTestRun]
        [AfterFeature("chrome")]
        public static void RemoveFakes()
        {
            WebServer.RemoveFakes();
            TearDownChrome();
        }

        [BeforeFeature("chrome")]
        public static void SetupChrome()
        {
            Console.WriteLine("Starting iisexpress...");
            WebServer.StartIis();

            if (FeatureWrapper.PhantomJsDriver == null)
            {
                var phantomDir = ConfigurationManager.AppSettings["PhantomJsDirectory"];
                FeatureWrapper.PhantomJsDriver = new PhantomJSDriver(phantomDir);
                FeatureWrapper.JQuery = new JQuery(FeatureWrapper.PhantomJsDriver);
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

        [AfterScenario("reset_after_scenario")]
        public static void ResetAfterScenario()
        {
            MongoWrapper.Instance.Reset();
        }

        public static void TearDownChrome()
        {
            try
            {
                FeatureWrapper.PhantomJsDriver.Quit();
                FeatureWrapper.PhantomJsDriver.Dispose();
            }
            catch (Exception e)
            {
                // ignore
            }
        }
    }
}
