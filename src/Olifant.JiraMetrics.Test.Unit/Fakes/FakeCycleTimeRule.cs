using System;

using Olifant.JiraMetrics.Lib.Jira.Model;
using Olifant.JiraMetrics.Lib.Metrics;

namespace Olifant.JiraMetrics.Test.Unit.Fakes
{
    public class FakeCycleTimeRule : CycleTimeRule
    {
        private readonly DateTime startDateTime;

        private readonly DateTime doneDateTime;

        public FakeCycleTimeRule(string startDateTime, string doneDateTime)
            : base(new string[] { }, new string[] { }, string.Empty)
        {
            this.startDateTime = DateTime.Parse(startDateTime);
            this.doneDateTime = DateTime.Parse(doneDateTime);
        }

        public override DateTime? GetDoneDateTime(Issue issue)
        {
            return this.doneDateTime;
        }

        public override DateTime? GetStartDateTime(Issue issue)
        {
            return this.startDateTime;
        }
    }
}