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
    [NUnit.Framework.DescriptionAttribute("JM6 - Make it possible to assign any existing statuses to cycle time")]
    [NUnit.Framework.CategoryAttribute("web")]
    public partial class JM6_MakeItPossibleToAssignAnyExistingStatusesToCycleTimeFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "JM6 - Make it possible to assign any existing statuses to cycle time.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "JM6 - Make it possible to assign any existing statuses to cycle time", "To be able to work with cycle times\r\nAs a person involved in Jira project\r\nI need" +
                    " ways to define what statuses should be in the cycle time", ProgrammingLanguage.CSharp, new string[] {
                        "web"});
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
        [NUnit.Framework.DescriptionAttribute("Present cycle statuses")]
        public virtual void PresentCycleStatuses()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Present cycle statuses", ((string[])(null)));
#line 8
this.ScenarioSetup(scenarioInfo);
#line 9
 testRunner.Given("I am logged in as \"Andreas\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 10
 testRunner.When("I navigate to \"burn-up\" page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 11
 testRunner.And("I make the statuses visible", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Precycle Status",
                        "Cycle status",
                        "Postcycle Status"});
            table1.AddRow(new string[] {
                        "Open",
                        "System Test",
                        "Deployed In Acceptance Test"});
            table1.AddRow(new string[] {
                        "Reopened",
                        "Ready for Test",
                        "System Integration Test"});
            table1.AddRow(new string[] {
                        "",
                        "Describing Requirement",
                        "System Integration Testing"});
            table1.AddRow(new string[] {
                        "",
                        "Build & Configure",
                        "Resolved"});
            table1.AddRow(new string[] {
                        "",
                        "Building & Configuring",
                        "Closed"});
            table1.AddRow(new string[] {
                        "",
                        "System Testing",
                        "Acceptance Test"});
            table1.AddRow(new string[] {
                        "",
                        "Describe Requirement",
                        "Acceptance Testing"});
            table1.AddRow(new string[] {
                        "",
                        "Review",
                        "System Test Done"});
            table1.AddRow(new string[] {
                        "",
                        "",
                        "Design Architecture"});
            table1.AddRow(new string[] {
                        "",
                        "",
                        "Designing Architecture"});
            table1.AddRow(new string[] {
                        "",
                        "",
                        "Implement"});
            table1.AddRow(new string[] {
                        "",
                        "",
                        "Implementing"});
            table1.AddRow(new string[] {
                        "",
                        "",
                        "Test"});
            table1.AddRow(new string[] {
                        "",
                        "",
                        "Testing"});
            table1.AddRow(new string[] {
                        "",
                        "",
                        "Deployed In Test"});
#line 12
 testRunner.Then("I should see default setup for statuses in cycle:", ((string)(null)), table1, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Hide statuses")]
        public virtual void HideStatuses()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Hide statuses", ((string[])(null)));
#line 31
this.ScenarioSetup(scenarioInfo);
#line 32
 testRunner.Given("I am logged in as \"Andreas\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 33
 testRunner.And("I navigate to \"burn-up\" page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
 testRunner.When("I hide the statuses", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 35
 testRunner.Then("the statuses should be hidden", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Move statuses")]
        public virtual void MoveStatuses()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Move statuses", ((string[])(null)));
#line 38
this.ScenarioSetup(scenarioInfo);
#line 39
 testRunner.Given("I am logged in as \"Andreas\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 40
 testRunner.And("I navigate to \"burn-up\" page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
 testRunner.When("I make the statuses visible", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 42
 testRunner.And("I move status \"Build & Configure\" in \"Cycle Statuses\" to \"Pre Cycle Statuses\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 43
 testRunner.And("I move status \"Reopened\" in \"Pre Cycle Statuses\" to \"Cycle Statuses\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 44
 testRunner.And("I move status \"Describe Requirement\" in \"Cycle Statuses\" to \"Post Cycle statuses\"" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 45
 testRunner.And("I move status \"Closed\" in \"Post Cycle Statuses\" to \"Cycle statuses\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Precycle Status",
                        "Cycle status",
                        "Postcycle Status"});
            table2.AddRow(new string[] {
                        "Open",
                        "System Test",
                        "Deployed In Acceptance Test"});
            table2.AddRow(new string[] {
                        "Build & Configure",
                        "Ready for Test",
                        "System Integration Test"});
            table2.AddRow(new string[] {
                        "",
                        "Describing Requirement",
                        "System Integration Testing"});
            table2.AddRow(new string[] {
                        "",
                        "Building & Configuring",
                        "Resolved"});
            table2.AddRow(new string[] {
                        "",
                        "System Testing",
                        "Acceptance Test"});
            table2.AddRow(new string[] {
                        "",
                        "Review",
                        "Acceptance Testing"});
            table2.AddRow(new string[] {
                        "",
                        "Reopened",
                        "System Test Done"});
            table2.AddRow(new string[] {
                        "",
                        "Closed",
                        "Design Architecture"});
            table2.AddRow(new string[] {
                        "",
                        "",
                        "Designing Architecture"});
            table2.AddRow(new string[] {
                        "",
                        "",
                        "Implement"});
            table2.AddRow(new string[] {
                        "",
                        "",
                        "Implementing"});
            table2.AddRow(new string[] {
                        "",
                        "",
                        "Test"});
            table2.AddRow(new string[] {
                        "",
                        "",
                        "Testing"});
            table2.AddRow(new string[] {
                        "",
                        "",
                        "Deployed In Test"});
            table2.AddRow(new string[] {
                        "",
                        "",
                        "Describe Requirement"});
#line 46
 testRunner.Then("I should see following setup for statuses in cycle:", ((string)(null)), table2, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
