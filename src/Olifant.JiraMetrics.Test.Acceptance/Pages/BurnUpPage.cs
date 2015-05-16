using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;

namespace Olifant.JiraMetrics.Test.Acceptance.Pages
{
    public sealed class BurnUpPage : PageObject
    {
        public BurnUpPage(PhantomJSDriver driver) : base(driver)
        {
        }

        protected override string VirtualPath { get { return ""; } }

        public string ProjectDropdown
        {
            get
            {
                return JQuery.Find("#ProjectList").Text();
            }
        }

        public bool ChartDivContains(string expectedText)
        {
            return TryUntilTimeout(() =>
            {
                var chartDiv = JQuery.Find("#chartDiv");
                return chartDiv.Text().Contains(expectedText);
            });
        }

        public bool ChartContains(IEnumerable<string> expectedTexts)
        {
            return ChartContains(expectedTexts.ToArray());
        }

        public bool ChartContains(params string[] expectedTexts)
        {
            return TryUntilTimeout(() =>
            {
                var chartContainer = JQuery.Find("#chartDiv");
                return expectedTexts.All(text => chartContainer.Text().Contains(text));
            });
        }

        public void SearchForProject(string project)
        {
            var projectDropdown = Driver.FindElementById("ProjectList");

            new SelectElement(projectDropdown)
                .SelectByText(project);

            JQuery.Find("#Search").Click();
        }

        public void ToggleStatuses()
        {
            ClickTheButton("toggleStatusVisibility");
        }

        public void MoveStatus(string status, string @from, string to)
        {
            // TODO: naming convention coming up... hodoesitwork? :-/
            var fromListboxName = from.Replace(" ", string.Empty) + "Listbox";
            var fromListboxControl = new SelectElement(Driver.FindElement(By.Name(fromListboxName)));
            fromListboxControl.SelectByText(status);

            var buttonId = CreateButtonIdFromSpec(@from, to);
            ClickTheButton(buttonId);
        }

        private void ClickTheButton(string buttonId)
        {
            var button = Driver.FindElement(By.Id(buttonId));
            button.Click();
        }

        private static string CreateButtonIdFromSpec(string from, string to)
        {
            var buttonName = string.Format(
                "from{0}To{1}Button",
                CultureInfo.CurrentCulture.TextInfo.ToTitleCase(from).Replace(" ", string.Empty),
                CultureInfo.CurrentCulture.TextInfo.ToTitleCase(to).Replace(" ", string.Empty));
            return buttonName;
        }

        public bool PreCycleStatusesContains(List<string> expectedPreCycleStatuses)
        {
            return StatusesContains(expectedPreCycleStatuses, "#PreCycleStatusesListBox");
        }

        public bool CycleStatusesContains(List<string> expectedCycleStatuses)
        {
            return StatusesContains(expectedCycleStatuses, "#CycleStatusesListBox");
        }

        public bool PostCycleStatusesContains(List<string> expectedPostCycleStatuses)
        {
            return StatusesContains(expectedPostCycleStatuses, "#PostCycleStatusesListBox");
        }

        private bool StatusesContains(List<string> expectedPostCycleStatuses, string listBoxId)
        {
            return TryUntilTimeout(() =>
            {
                var actualStatuses = JQuery.Find(listBoxId).Text();
                return expectedPostCycleStatuses.All(text => actualStatuses.Contains(text));
            });
        }
    }
}