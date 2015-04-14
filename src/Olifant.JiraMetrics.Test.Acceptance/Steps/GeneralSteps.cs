using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Olifant.JiraMetrics.Lib;
using Olifant.JiraMetrics.Lib.Metrics.Filters;
using Olifant.JiraMetrics.Test.Acceptance.Steps.Helpers;
using Olifant.JiraMetrics.Test.Acceptance.Steps.Specs;
using Olifant.JiraMetrics.Test.Utilities.Fakes;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Olifant.JiraMetrics.Test.Acceptance.Steps
{
    [Binding]
    public class GeneralSteps
    {
        private const int HugeChunkSizeToPreventMultipleCallsOfSameStubFile = 10000;

        [Given(@"the checkbox ""(.*)"" is ""(.*)""")]
        public void GivenTheCheckboxIs(string checkBoxName, string checkBoxValue)
        {
            var givenCheckBoxValue = new CheckBoxSpec{ IsChecked = checkBoxValue };
            switch (checkBoxName)
            {
            case "Save query":
                ScenarioWrapper.SaveQuery = givenCheckBoxValue.IsTrue;
                break;
            default:
                throw new NotImplementedException(string.Format("Checkbox {0} not implemented yet", checkBoxName));
            }
        }

        [Given(@"program is started")]
        [When(@"I start the program")]
        [When(@"I restart the program")]
        public void StartProgram()
        {
            ScenarioWrapper.FakeTextEditorProxy = new FakeTextEditorProxy();
            ScenarioWrapper.JiraCyclesManager = new JiraMetricsFacade(new FakeJiraRestClient());
        }

        [When(@"I click the ""(.*)"" button")]
        public void WhenIClickTheButton(string buttonName)
        {
            var filters = new List<IIssueFilter>()
                    {
                        ScenarioWrapper.StartDateTimeFilter
                    };

            if (ScenarioWrapper.WorkDoneFilter != null)
            {
                filters.Add(ScenarioWrapper.WorkDoneFilter);
            }

            if (ScenarioWrapper.DoneDateTimeFilter != null)
            {
                filters.Add(ScenarioWrapper.DoneDateTimeFilter);
            }

            switch (buttonName) 
            {
                case "Cycle time report":
                    ScenarioWrapper.FakeTextEditorProxy = new FakeTextEditorProxy();
                    ScenarioWrapper.JiraCyclesManager.GenerateCycleTimeReport(
                        ScenarioWrapper.Jql,
                        ScenarioWrapper.CycleTimeRule,
                        filters,
                        HugeChunkSizeToPreventMultipleCallsOfSameStubFile,
                        ScenarioWrapper.FakeTextEditorProxy);
                    break;
                case "Value added time report":
                    ScenarioWrapper.FakeTextEditorProxy = new FakeTextEditorProxy();
                    ScenarioWrapper.JiraCyclesManager.GenerateValueAddedTimeReport(
                        ScenarioWrapper.Jql,
                        ScenarioWrapper.CycleTimeRule,
                        filters,
                        HugeChunkSizeToPreventMultipleCallsOfSameStubFile,
                        ScenarioWrapper.FakeTextEditorProxy);
                    break;
                default:
                    Console.WriteLine("--> pending: Button {0} not implemented yet", buttonName);
                    ScenarioContext.Current.Pending();
                    break;
            }
        }

        [Then(@"I should see the following controls:")]
        public void VerifyControls(Table table)
        {
            var expectedControls = table.CreateSet<WebControlSpec>().ToList();

            expectedControls.ForEach(
                controlSpec =>
                {
                    FeatureWrapper.JQuery.Find("#" + controlSpec.Name)
                        .Should().NotBeNull();
                });
        }
    }
}