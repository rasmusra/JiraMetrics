using System;
using System.Configuration;

using Olifant.JiraMetrics.Test.Acceptance.Steps.Helpers;

using OpenQA.Selenium.PhantomJS;

using TechTalk.SpecFlow;

namespace Olifant.JiraMetrics.Test.Acceptance
{
    [Binding]
    public class Hooks
    {
        [BeforeTestRun]
        public static void SetupFakes()
        {
            WebServer.SetupFakes();
        }

        [AfterTestRun]
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
                Console.WriteLine(phantomDir);
                FeatureWrapper.PhantomJsDriver = new PhantomJSDriver(phantomDir);
            }
        }

        [BeforeFeature]
        public static void MakeFeatureLogReadableBefore()
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("Feature: " + FeatureContext.Current.FeatureInfo.Title);
            Console.WriteLine(FeatureContext.Current.FeatureInfo.Description);
            Console.WriteLine(new string('-', 40));
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
