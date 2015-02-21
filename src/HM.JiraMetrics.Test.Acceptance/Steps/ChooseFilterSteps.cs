using System;
using System.Linq;

using FluentAssertions;

using HM.JiraMetrics.Lib.Metrics;
using HM.JiraMetrics.Lib.Metrics.Filters;
using HM.JiraMetrics.Test.Acceptance.Steps.Helpers;
using HM.JiraMetrics.Test.Fakes.Helpers;

using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace HM.JiraMetrics.Test.Acceptance.Steps
{
    [Binding]
    internal class ChooseFilterSteps
    {
        [Given(@"I ""(.*)"" the checkbox ""(.*)""")]
        public void SetCheckbox(string checkBoxValue, string checkboxName)
        {
            if (checkboxName == "Exclude issues that are not done")
            {
                ScenarioWrapper.WorkDoneFilter = new CheckBoxSpec{ IsChecked = checkBoxValue }.IsTrue ? new WorkDoneFilter() : null;
            }
            else
            {
                throw new NotImplementedException("checkbox not handled: " + checkboxName);
            }
        }

        [Given(@"I have chosen the start and end of cycle as ""(.*)""")]
        public void SetCycleTimeRule(string givenStartOfCycle)
        {
            ScenarioWrapper.CycleTimeRule = CreateCycleTimeRuleFromSpec(givenStartOfCycle);
        }

        [Given(@"I have chosen start label ""(.*)"" and start and end of cycle as ""(.*)""")]
        public void SetCycleTimeRule(string startLabel, string givenStartOfCycle)
        {
            ScenarioWrapper.CycleTimeRule = CreateCycleTimeRuleFromSpec(givenStartOfCycle, startLabel);
        }

        [Given(@"I enter the Done date interval as:")]
        public void SetDoneDateFilter(Table table)
        {
            ScenarioWrapper.DoneDateTimeFilter = table.CreateInstance<DateFilterSpec>().CreateDoneDateFilter();
        }

        [Given(@"I enter the Start date interval as:")]
        public void SetStartDateFilter(Table table)
        {
            ScenarioWrapper.StartDateTimeFilter = table.CreateInstance<DateFilterSpec>().CreateStartDateFilter();
        }

        [Then(@"I should be able to see (.*) defects in the report:")]
        public void ThenIShouldBeAbleToSeeDefectsInTheReport(int expectedNbrOfDefects)
        {
            ScenarioWrapper.FakeTextEditorProxy.ActualRowsExclHeader.Count()
            .ShouldBeEquivalentTo(expectedNbrOfDefects);
        }

        [Then(@"I should see default dates for Start date interval as:")]
        public void ThenIShouldSeeDefaultDatesForStartDateIntervalAs(Table table)
        {
            var expected = (StartDateFilter)table
                           .CreateInstance<DateFilterSpec>()
                           .CreateStartDateFilter();

            // settle test with verifying what values
            // that are set in the default constructor
            var actual = new StartDateFilter();

            actual.MinStartDateTime.Should().Be(expected.MinStartDateTime);
            actual.MaxStartDateTime.Should().Be(expected.MaxStartDateTime);
        }

        private static CycleTimeRule CreateCycleTimeRuleFromSpec(string givenCycleTimeRule, string givenStartLabel = "")
        {
            return new CycleTimeRule(CycleName2StatusesDict.Lookup(givenCycleTimeRule), givenStartLabel);
        }
    }
}