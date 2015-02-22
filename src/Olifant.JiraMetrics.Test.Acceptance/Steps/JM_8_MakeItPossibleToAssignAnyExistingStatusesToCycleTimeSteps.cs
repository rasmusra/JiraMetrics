using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;

using FluentAssertions;

using Olifant.JiraMetrics.Test.Acceptance.Steps.Helpers;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TechTalk.SpecFlow.Tracing;

namespace Olifant.JiraMetrics.Test.Acceptance.Steps
{
    [Binding]
    public class JM_8_MakeItPossibleToAssignAnyExistingStatusesToCycleTimeSteps
    {
        [Then(@"I should be able to see the following header in the report: ""(.*)""")]
        public void ThenIShouldBeAbleToSeeTheFollowingHeaderInTheReport(string expectedHeader)
        {
            ScenarioWrapper.FakeTextEditorProxy.ActualHeader.ShouldBeEquivalentTo(expectedHeader);
        }

        [Then(@"I should see default setup for statuses in cycle:")]
        [Then(@"I should see following setup for statuses in cycle:")]
        public void ThenIShouldSeeDefaultSetupForStatusesInCycle(Table table)
        {
            var cycleSetupManager = new CycleSetupManager(table.CreateSet<CycleSetupSpec>());

            VerifyStatuses(cycleSetupManager.PreCycleStatuses, "PreCycleStatusesListbox");
            VerifyStatuses(cycleSetupManager.CycleStatuses, "CycleStatusesListbox");
            VerifyStatuses(cycleSetupManager.PostCycleStatuses, "PostCycleStatusesListbox");
        }

        private static void VerifyStatuses(List<string> expectedCycleStatuses, string webControlName)
        {
            var actualStatuses = FeatureWrapper.PhantomJsDriver.FindElement(By.Name(webControlName));
            expectedCycleStatuses.ForEach(expectedStatus =>
                actualStatuses.Text.Should().Contain(expectedStatus));
        }

        [When(@"I move status ""(.*)"" in ""(.*)"" to ""(.*)""")]
        public void MoveStatus(string status, string from, string to)
        {
            var fromListboxName = from.Replace(" ", string.Empty) + "Listbox";
            var fromListboxControl = new SelectElement(FeatureWrapper.PhantomJsDriver.FindElement(By.Name(fromListboxName)));
            fromListboxControl.SelectByText(status);

            var buttonId = CreateButtonIdFromSpec(@from, to);

            this.ClickTheButton(buttonId);

        }

        [When(@"I click the button ""(.*)""")]
        public void ClickTheButton(string buttonId)
        {
            var button = FeatureWrapper.PhantomJsDriver.FindElement(By.Id(buttonId));
            button.Click();
        }

        private static string CreateButtonIdFromSpec(string @from, string to)
        {
            var buttonName = string.Format(
                "from{0}To{1}Button",
                CultureInfo.CurrentCulture.TextInfo.ToTitleCase(@from).Replace(" ", string.Empty),
                CultureInfo.CurrentCulture.TextInfo.ToTitleCase(to).Replace(" ", string.Empty));
            return buttonName;
        }
    }

    public class CycleSetupManager
    {
        private readonly IEnumerable<CycleSetupSpec> cycleSetupSpecs;

        public CycleSetupManager(IEnumerable<CycleSetupSpec> cycleSetupSpecs)
        {
            this.cycleSetupSpecs = cycleSetupSpecs;
        }

        public List<string> PreCycleStatuses
        {
            get
            {
                return this.GetStatuses(spec => spec.PrecycleStatus);
            }
        }

        public List<string> CycleStatuses
        {
            get
            {
                return this.GetStatuses(spec => spec.CycleStatus);
            }
        }

        public List<string> PostCycleStatuses
        {
            get
            {
                return this.GetStatuses(spec => spec.PostcycleStatus);
            }
        }

        public string GetSpecifiedListBoxtext(IEnumerable<string> texts)
        {
            return string.Join(" ", texts.ToArray());
        }

        private List<string> GetStatuses(Func<CycleSetupSpec, string> specField)
        {
            return this.cycleSetupSpecs
                .Select(specField)
                .Where(field => !string.IsNullOrEmpty(field))
                .ToList();
        }
    }

    public class CycleSetupSpec
    {
        public string PrecycleStatus { get; set; }
        
        public string CycleStatus { get; set; }
        
        public string PostcycleStatus { get; set; }
    }
}