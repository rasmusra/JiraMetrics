using System;

using Olifant.JiraMetrics.Lib.Metrics.Model;

using Moq;

namespace Olifant.JiraMetrics.Test.Unit.Mocks
{
    public class IssueReportModelMock
    {
        public static IIssueReportModel Create(string storyPoints, string doneDate)
        {
            return Create(storyPoints, DateTime.MinValue.ToString(), doneDate);
        }

        public static IIssueReportModel Create(string storyPoints, string startDate, string doneDate)
        {
            var mock = new Mock<IIssueReportModel>();
            mock.Setup(irm => irm.StoryPoints).Returns(storyPoints);
            mock.Setup(irm => irm.DoneDateTime).Returns(startDate);
            mock.Setup(irm => irm.DoneDateTime).Returns(doneDate);
            return mock.Object;
        }
    }
}