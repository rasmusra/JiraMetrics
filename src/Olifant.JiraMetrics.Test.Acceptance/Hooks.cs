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
        [BeforeFeature]
        public static void SetUp()
        {
            Console.WriteLine("Hooks: Kill running processes...");
            IisExpressManager.Kill();
            WinProcessWrapper.KillByName("phantomjs");

            Console.WriteLine("Hooks: Setup db...");
            MongoWrapper.Init(ConfigurationManager.AppSettings["ConnectionString"],
                IssueStubFactory.CreateFromFiles());

            Console.WriteLine("Hooks: Setup web...");
            IisExpressManager.SetupFakes("FakeStructureMap.xml");
        }

        [BeforeFeature]
        public static void MakeFeatureLogReadableBefore()
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("Feature: " + FeatureContext.Current.FeatureInfo.Title);
            Console.WriteLine(FeatureContext.Current.FeatureInfo.Description);
            Console.WriteLine();
        }

        /// <summary>
        /// Add tag @web to setup an iisexpress server during scenario
        /// </summary>
        #region web

        [BeforeScenario("web")]
        public static void SetupBrowser()
        {
            Console.WriteLine("Starting iisexpress...");
            IisExpressManager.Start();

            if (FeatureWrapper.PhantomJsDriver == null)
            {
                var phantomDir = ConfigurationManager.AppSettings["PhantomJsDirectory"];
                FeatureWrapper.PhantomJsDriver = new PhantomJSDriver(phantomDir, new PhantomJSOptions(), TimeSpan.FromSeconds(60));
                FeatureWrapper.JQuery = new JQuery(FeatureWrapper.PhantomJsDriver);
            }
        }

        [AfterScenario("web")]
        public static void TearDown()
        {
            Console.WriteLine("Hooks: Tear down web...");
            IisExpressManager.RemoveFakes();
            IisExpressManager.Kill();

            try
            {
                FeatureWrapper.PhantomJsDriver.Quit();
                FeatureWrapper.PhantomJsDriver.Dispose();
                FeatureWrapper.PhantomJsDriver = null;
                WinProcessWrapper.KillByName("phantomjs");
            }
            catch (Exception e)
            {
                Console.Write("Problems when tearing down phantomjs: " + e);
            }
        }

        #endregion

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
            ScenarioWrapper.DbNeedstoBeResetAfterScenario = false;
        }

        [AfterScenario]
        public static void ResetChangedDataAfterScenario()
        {
            if (ScenarioWrapper.DbNeedstoBeResetAfterScenario)
            {
                MongoWrapper.Instance.InitTestPopulation(IssueStubFactory.CreateFromFiles());
            }

            ScenarioWrapper.DbNeedstoBeResetAfterScenario = true;
        }
    }
}
