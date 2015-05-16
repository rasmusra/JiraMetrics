using System.Collections.Generic;
using System.Linq;
using Olifant.JiraMetrics.Test.Acceptance.Steps.Specs;
using OpenQA.Selenium.PhantomJS;

namespace Olifant.JiraMetrics.Test.Acceptance.Pages
{
    public class AdminPage : PageObject
    {
        public AdminPage(PhantomJSDriver driver) : base(driver)
        {
        }

        protected override string VirtualPath { get { return "/Admin"; }  }

        public void Load(string project)
        {
            JQuery.Find("#ProjectTextBox").Val(project);
            JQuery.Find("#LoadIssues").Click();
        }

        public bool LoadedIssuesReportContains(IEnumerable<LoadedIssueSpec> expectedLoadedIssues)
        {
            return TryUntilTimeout(() =>
            {
                var actualLoadedIssues = JQuery.Find("#LoadedIssueListControl").Text();
                var loadedIssueSpecs = expectedLoadedIssues as IList<LoadedIssueSpec> ?? expectedLoadedIssues.ToList();

                var containsKey = loadedIssueSpecs.All(expectedLoadedIssue => actualLoadedIssues.Contains(expectedLoadedIssue.Issue));
                var containsAction = loadedIssueSpecs.All(expectedLoadedIssue => actualLoadedIssues.Contains(expectedLoadedIssue.Action));

                return containsAction && containsKey;
            });
        }

        public bool LoadedIssuesReportContains(string expectedMessage)
        {
            return TryUntilTimeout(() =>
            {
                var actualLoadedIssuesReport = JQuery.Find("#LoadedIssueListControl").Text();
                return actualLoadedIssuesReport.Contains(expectedMessage);
            });
        }
    }
}