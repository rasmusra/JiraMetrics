using System;
using System.Diagnostics;
using System.Linq;

using Olifant.JiraMetrics.Lib.Jira.Model;

namespace Olifant.JiraMetrics.Lib.Metrics
{
    [DebuggerDisplay("Statuses={Statuses}")]
    public class CycleTimeRule
    {
        private readonly string startedLabel;

        public CycleTimeRule(Status[] statuses)
            : this(statuses, new Status[] { }, string.Empty)
        {
        }

        public CycleTimeRule(Status[] statuses, string startedLabel)
            : this(statuses, new Status[] { }, startedLabel)
        {
        }

        public CycleTimeRule(Status[] statuses, Status[] preCycleStatuses)
            : this(statuses, preCycleStatuses, string.Empty)
        {
        }

        public CycleTimeRule(Status[] statuses, Status[] preCycleStatuses, string startedLabel)
        {
            this.startedLabel = startedLabel;
            this.PreCycleStatuses = preCycleStatuses;
            this.Statuses = statuses;
        }

        public Status[] Statuses { get; private set; } 

        /// <summary>
        /// Gets all statuses preceding the cycle time of derived class. This one is necessary 
        /// in order to determine if a case is closed or re-opened (w.r.t. cycle statuses)
        /// </summary>
        public Status[] PreCycleStatuses { get; private set; }

        public virtual DateTime? GetStartDateTime(Issue issue)
        {
            DateTime? startDate;

            var dateWhenItemWasLabelledAsStarted = issue.GetDateWhenItemWasLabelledAsStarted(this.startedLabel);
            var cycleStartedHistory = issue.ChangeLog.FirstHistoryMovedToStatus(this.Statuses);
            var dateWhenCycleWasEntered = cycleStartedHistory != null
                ? (DateTime?)DateTime.Parse(cycleStartedHistory.Created)
                : null;

            if (dateWhenItemWasLabelledAsStarted != null && dateWhenCycleWasEntered != null)
            {
                // the started label is set, but has also entered a cycle status.
                // it is possible to set the started label at any time, so we need to return the first occasion
                startDate = dateWhenItemWasLabelledAsStarted < dateWhenCycleWasEntered
                           ? dateWhenItemWasLabelledAsStarted
                           : dateWhenCycleWasEntered;
            }
            else if (dateWhenItemWasLabelledAsStarted != null)
            {
                // If the cycle statuses has not been entered but the started-label is set, 
                // then the cycle is said to have been started.
                startDate = dateWhenItemWasLabelledAsStarted;
            }
            else if (this.Statuses.ToList().Exists(s => s.Name.Equals("open", StringComparison.OrdinalIgnoreCase)))
            {
                startDate = DateTime.Parse(issue.Fields.Created);
            }
            else
            {
                startDate = dateWhenCycleWasEntered;
            }

            return startDate;
        }

        public virtual DateTime? GetDoneDateTime(Issue issue)
        {
            if (this.IsToDo(issue) || this.IsDoing(issue))
            {
                return null;
            }

            var doneHistory = issue.ChangeLog.LastHistoryMovedFromStatus(this.Statuses);

            var doneDate = (doneHistory != null)
                ? DateTime.Parse(doneHistory.Created)
                : (DateTime?)null;

            return doneDate;
        }

        public override string ToString()
        {
            return string.Join(", ", this.Statuses.Select(s => s.Name));
        }

        private bool IsToDo(Issue issue)
        {
            if (issue.Fields.Status.IsOpen && !issue.HasLabel(this.startedLabel))
            {
                // this happens when issue is open but has not been started with the started-label
                return false;
            }

            return this.PreCycleStatuses.Contains(issue.Fields.Status);
        }

        private bool IsDoing(Issue issue)
        {
            if (issue.Fields.Status.IsOpen && issue.HasLabel(this.startedLabel))
            {
                // this happens when issue is open AND has been started with the started-label
                return true;
            }

            return this.Statuses.Contains(issue.Fields.Status);
        }
    }
}