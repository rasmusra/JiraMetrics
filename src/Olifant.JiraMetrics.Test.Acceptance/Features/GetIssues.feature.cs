﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.34014
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Olifant.JiraMetrics.Test.Acceptance.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("GetCurrent issues")]
    public partial class GetIssuesFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "GetIssues.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "GetCurrent issues", "In order to get a sense of how long time it takes to fix issues\r\nAs a person invo" +
                    "lved in Jira project\r\nI need ways to see cycle times", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("GetCurrent all defects from DISCO")]
        public virtual void GetAllDefectsFromDISCO()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("GetCurrent all defects from DISCO", ((string[])(null)));
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("program is started", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.And("I have entered the following query: \"key in (DISCO-766,DISCO-767,DISCO-636)\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
 testRunner.And("I have chosen start label \"started\" and start and end of cycle as \"Full process\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 11
 testRunner.When("I click the \"Cycle time report\" button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 12
 testRunner.Then("I should be presented all defects in a text file", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Scenario description",
                        "Key",
                        "Issue type",
                        "Summary",
                        "Started date",
                        "Closed date",
                        "Cycle time",
                        "Story Points",
                        "Original estimate"});
            table1.AddRow(new string[] {
                        "Started when defect was created",
                        "DISCO-766",
                        "Defect",
                        "Removed articles on manufacturing option not working",
                        "2014-09-12 13:46:36",
                        "2014-09-16 13:53:42",
                        "4,00",
                        "2,0",
                        ""});
            table1.AddRow(new string[] {
                        "Started a few days after creation date",
                        "DISCO-767",
                        "Defect",
                        "SIT - Notice error exception in PMT",
                        "2014-09-16 15:35:12",
                        "2014-09-18 11:04:26",
                        "1,81",
                        "0,5",
                        ""});
#line 13
 testRunner.And("I should be able to see the following cycle times in the report:", ((string)(null)), table1, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Different start and end date combinations")]
        [NUnit.Framework.TestCaseAttribute("Started, but without the started-flag", "OFU-676", "Defect", "Re-filtering page leaves you on same \"page number\" as previously - should go to f" +
            "irst page", "2013-02-25 09:41:34", "2013-02-27 16:49:34", "2,30", "1,0", null)]
        [NUnit.Framework.TestCaseAttribute("Started, with some time in arch design", "OFU-2377", "Defect", "UI fixes", "2014-09-25 08:30:53", "2014-10-08 09:24:16", "13,04", "1,5", null)]
        [NUnit.Framework.TestCaseAttribute("Moved to TCC when done", "OFU-1462", "Change Request - Functional", "Update date filter functionality and look in Order list page", "2013-08-16 16:02:06", "2013-08-23 04:25:53", "6,52", "1,0", null)]
        [NUnit.Framework.TestCaseAttribute("Directly moved from open to testing", "OFU-2193", "Defect", "Sprint test- Pending preadvice not displayed on Pending tab", "2014-06-09 13:42:37", "2014-06-10 07:49:41", "0,75", "0,0", null)]
        [NUnit.Framework.TestCaseAttribute("Started before the \"started\" label was added", "OFU-2290", "Defect Sub-Task", "Incorrect position of the Advanced Filter header", "2014-08-27 09:08:33", "2014-12-18 07:45:20", "112,94", "1,0", null)]
        public virtual void DifferentStartAndEndDateCombinations(string scenarioDescription, string key, string issueType, string summary, string startedDate, string closedDate, string cycleTime, string storyPoints, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Different start and end date combinations", exampleTags);
#line 21
this.ScenarioSetup(scenarioInfo);
#line 22
 testRunner.Given("program is started", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 23
 testRunner.And(string.Format("I have entered the following query: \"Key={0}\"", key), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 24
 testRunner.And("I have chosen start label \"started\" and start and end of cycle as \"Full process\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 25
 testRunner.When("I click the \"Cycle time report\" button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Key",
                        "Issue type",
                        "Summary",
                        "Started date",
                        "Closed date",
                        "Cycle Time",
                        "Story Points",
                        "Original estimate"});
            table2.AddRow(new string[] {
                        string.Format("{0}", key),
                        string.Format("{0}", issueType),
                        string.Format("{0}", summary),
                        string.Format("{0}", startedDate),
                        string.Format("{0}", closedDate),
                        string.Format("{0}", cycleTime),
                        string.Format("{0}", storyPoints),
                        ""});
#line 26
 testRunner.Then("I should be able to see the following cycle times in the report:", ((string)(null)), table2, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Issues that should be filtered out")]
        [NUnit.Framework.TestCaseAttribute("Opened but closed before work started", "Dev", "", "SCSC-974", null)]
        [NUnit.Framework.TestCaseAttribute("Development not done yet", "Dev", "", "DISCO-729", null)]
        [NUnit.Framework.TestCaseAttribute("Started but reopened", "Full process", "started", "DISCO-838", null)]
        [NUnit.Framework.TestCaseAttribute("In requirement phase", "Full process", "started", "OFU-2299", null)]
        public virtual void IssuesThatShouldBeFilteredOut(string scenarioDescription, string cycleTime, string startLabel, string keyThatShouldBeFilteredOut, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Issues that should be filtered out", exampleTags);
#line 39
this.ScenarioSetup(scenarioInfo);
#line 40
 testRunner.Given("program is started", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 41
 testRunner.And(string.Format("I have entered the following query: \"Key={0}\"", keyThatShouldBeFilteredOut), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 42
 testRunner.And(string.Format("I have chosen start label \"{0}\" and start and end of cycle as \"{1}\"", startLabel, cycleTime), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 43
 testRunner.And("I \"check\" the checkbox \"Exclude issues that are not done\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 44
 testRunner.When("I click the \"Cycle time report\" button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 45
 testRunner.Then(string.Format("I should not see \"{0}\" in the report", keyThatShouldBeFilteredOut), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Handle failed query")]
        [NUnit.Framework.IgnoreAttribute()]
        public virtual void HandleFailedQuery()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Handle failed query", new string[] {
                        "ignore"});
#line 56
this.ScenarioSetup(scenarioInfo);
#line 57
 testRunner.Given("program is started", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 58
 testRunner.And("I have entered the following query: \"THIS IS A REALLY BAD QUERY\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 59
 testRunner.Then("I should be presented an error message", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 60
 testRunner.And("I should be directed back to query window", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
