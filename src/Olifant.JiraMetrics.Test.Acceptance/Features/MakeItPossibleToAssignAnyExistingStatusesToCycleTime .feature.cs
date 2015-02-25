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
    [NUnit.Framework.DescriptionAttribute("Make it possible to assign any existing statuses to cycle time")]
    [NUnit.Framework.CategoryAttribute("chrome")]
    public partial class MakeItPossibleToAssignAnyExistingStatusesToCycleTimeFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "MakeItPossibleToAssignAnyExistingStatusesToCycleTime .feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Make it possible to assign any existing statuses to cycle time", "For being able to work with cycle times\r\nAs a person involved in Jira project\r\nI " +
                    "need the exported cycle times presented in a report", ProgrammingLanguage.CSharp, new string[] {
                        "chrome"});
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
        [NUnit.Framework.DescriptionAttribute("Show header in report")]
        public virtual void ShowHeaderInReport()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Show header in report", ((string[])(null)));
#line 8
this.ScenarioSetup(scenarioInfo);
#line 9
 testRunner.Given("program is started", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 10
 testRunner.And("I have entered the following query: \"Key=DUMMYKEY\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Cycle Periods"});
            table1.AddRow(new string[] {
                        "Design Architecture"});
            table1.AddRow(new string[] {
                        "Designing architecture"});
#line 11
 testRunner.And("I have chosen the cycle period as:", ((string)(null)), table1, "And ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Pre-cycle Periods"});
            table2.AddRow(new string[] {
                        "Open"});
            table2.AddRow(new string[] {
                        "Reopen"});
            table2.AddRow(new string[] {
                        "Describe Requirement"});
            table2.AddRow(new string[] {
                        "Describing Requirement"});
            table2.AddRow(new string[] {
                        "Requirement"});
            table2.AddRow(new string[] {
                        "In Analysis"});
#line 15
 testRunner.And("I have chosen the pre-cycle period as:", ((string)(null)), table2, "And ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Cycle",
                        "Label"});
            table3.AddRow(new string[] {
                        "Open",
                        "started"});
#line 23
 testRunner.And("I have stated the following labels as necessary for cycle:", ((string)(null)), table3, "And ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Min date",
                        "Max date"});
            table4.AddRow(new string[] {
                        "2000-01-01",
                        "2020-12-01"});
#line 26
 testRunner.And("I enter the Start date interval as:", ((string)(null)), table4, "And ");
#line 29
 testRunner.And("I \"check\" the checkbox \"Exclude issues that are not done\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 30
 testRunner.When("I click the \"Cycle time report\" button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 31
 testRunner.Then("I should be able to see the following header in the report: \"Cycle Time Report, c" +
                    "ycle: [Design Architecture, Designing Architecture], jql: Key=DUMMYKEY, filters:" +
                    " [StartDateFilter(2000-01-01, 2020-12-01), WorkDoneFilter]\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Present cycle statuses")]
        public virtual void PresentCycleStatuses()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Present cycle statuses", ((string[])(null)));
#line 33
this.ScenarioSetup(scenarioInfo);
#line 34
 testRunner.Given("I am logged in as \"Andreas\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 35
 testRunner.When("I navigate to burn-up page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 36
 testRunner.And("I make the statuses visible", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Precycle Status",
                        "Cycle status",
                        "Postcycle Status"});
            table5.AddRow(new string[] {
                        "Open",
                        "System Test",
                        "Deployed In Acceptance Test"});
            table5.AddRow(new string[] {
                        "Reopened",
                        "Ready for Test",
                        "System Integration Test"});
            table5.AddRow(new string[] {
                        "",
                        "Describing Requirement",
                        "System Integration Testing"});
            table5.AddRow(new string[] {
                        "",
                        "Build & Configure",
                        "Resolved"});
            table5.AddRow(new string[] {
                        "",
                        "Building & Configuring",
                        "Closed"});
            table5.AddRow(new string[] {
                        "",
                        "System Testing",
                        "Acceptance Test"});
            table5.AddRow(new string[] {
                        "",
                        "Describe Requirement",
                        "Acceptance Testing"});
            table5.AddRow(new string[] {
                        "",
                        "Review",
                        "System Test Done"});
            table5.AddRow(new string[] {
                        "",
                        "",
                        "Design Architecture"});
            table5.AddRow(new string[] {
                        "",
                        "",
                        "Designing Architecture"});
            table5.AddRow(new string[] {
                        "",
                        "",
                        "Implement"});
            table5.AddRow(new string[] {
                        "",
                        "",
                        "Implementing"});
            table5.AddRow(new string[] {
                        "",
                        "",
                        "Test"});
            table5.AddRow(new string[] {
                        "",
                        "",
                        "Testing"});
            table5.AddRow(new string[] {
                        "",
                        "",
                        "Deployed In Test"});
#line 37
 testRunner.Then("I should see default setup for statuses in cycle:", ((string)(null)), table5, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Hide statuses")]
        public virtual void HideStatuses()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Hide statuses", ((string[])(null)));
#line 56
this.ScenarioSetup(scenarioInfo);
#line 57
 testRunner.Given("I am logged in as \"Andreas\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 58
 testRunner.And("I navigate to burn-up page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 59
 testRunner.When("I hide the statuses", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 60
 testRunner.Then("the statuses should be hidden", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Move statuses")]
        public virtual void MoveStatuses()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Move statuses", ((string[])(null)));
#line 63
this.ScenarioSetup(scenarioInfo);
#line 64
 testRunner.Given("I am logged in as \"Andreas\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 65
 testRunner.And("I navigate to burn-up page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 66
 testRunner.When("I make the statuses visible", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 67
 testRunner.And("I move status \"Build & Configure\" in \"Cycle Statuses\" to \"Pre Cycle Statuses\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 68
 testRunner.And("I move status \"Reopened\" in \"Pre Cycle Statuses\" to \"Cycle Statuses\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 69
 testRunner.And("I move status \"Describe Requirement\" in \"Cycle Statuses\" to \"Post Cycle statuses\"" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 70
 testRunner.And("I move status \"Closed\" in \"Post Cycle Statuses\" to \"Cycle statuses\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Precycle Status",
                        "Cycle status",
                        "Postcycle Status"});
            table6.AddRow(new string[] {
                        "Open",
                        "System Test",
                        "Deployed In Acceptance Test"});
            table6.AddRow(new string[] {
                        "Build & Configure",
                        "Ready for Test",
                        "System Integration Test"});
            table6.AddRow(new string[] {
                        "",
                        "Describing Requirement",
                        "System Integration Testing"});
            table6.AddRow(new string[] {
                        "",
                        "Building & Configuring",
                        "Resolved"});
            table6.AddRow(new string[] {
                        "",
                        "System Testing",
                        "Acceptance Test"});
            table6.AddRow(new string[] {
                        "",
                        "Review",
                        "Acceptance Testing"});
            table6.AddRow(new string[] {
                        "",
                        "Reopened",
                        "System Test Done"});
            table6.AddRow(new string[] {
                        "",
                        "Closed",
                        "Design Architecture"});
            table6.AddRow(new string[] {
                        "",
                        "",
                        "Designing Architecture"});
            table6.AddRow(new string[] {
                        "",
                        "",
                        "Implement"});
            table6.AddRow(new string[] {
                        "",
                        "",
                        "Implementing"});
            table6.AddRow(new string[] {
                        "",
                        "",
                        "Test"});
            table6.AddRow(new string[] {
                        "",
                        "",
                        "Testing"});
            table6.AddRow(new string[] {
                        "",
                        "",
                        "Deployed In Test"});
            table6.AddRow(new string[] {
                        "",
                        "",
                        "Describe Requirement"});
#line 71
 testRunner.Then("I should see following setup for statuses in cycle:", ((string)(null)), table6, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
