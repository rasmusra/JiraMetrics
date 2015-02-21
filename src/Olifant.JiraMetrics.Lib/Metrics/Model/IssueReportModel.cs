using System;
using System.Globalization;
using System.Runtime.Remoting.Messaging;

using Olifant.JiraMetrics.Lib.Jira.Model;

namespace Olifant.JiraMetrics.Lib.Metrics.Model
{
    // TODO: extract general issue decorator to be reused in different issuereportmodel classes
    public class IssueReportModel : IIssueReportModel
    {
        private readonly Issue issue;

        public IssueReportModel(Issue issue, CycleTimeRule cycleTimeRule, bool countWithWeekends = true)
        {
            this.issue = issue;

            var startDateTime = cycleTimeRule.GetStartDateTime(issue);
            this.StartDateTime = this.ConvertNullableDateTimeToString(startDateTime);

            var doneDateTime = cycleTimeRule.GetDoneDateTime(issue);
            this.DoneDateTime = this.ConvertNullableDateTimeToString(doneDateTime);

            // TODO: too much done in constructor, clean up someway
            if (startDateTime != null && doneDateTime != null)
            {
                var totalDays = (doneDateTime - startDateTime).Value.TotalDays;

                if (!countWithWeekends)
                {
                    totalDays -= CountWeekendDays(totalDays, startDateTime.Value, doneDateTime.Value);
                }

                this.CycleTime = (decimal)Math.Round(totalDays, 2);
            }
        }


        // TODO: the separator belongs to the reportmanager more than the issuereportmodel
        public static char Separator
        {
            get
            {
                return '¤';
            }
        }

        public string DoneDateTime { get; private set; }

        public string StartDateTime { get; private set; }

        public decimal? CycleTime { get; private set; }

        public string StoryPoints
        {
            get
            {
                var formattedStoryPoints = issue.Fields.StoryPoints.HasValue
                                      ? issue.Fields.StoryPoints.Value.ToString("0.0", CultureInfo)
                                      : GetDefaultStoryPoint(issue.Fields.IssueType.Name);

                return formattedStoryPoints;
            }
        }

        private static int CountWeekendDays(double days, DateTime startDateTime, DateTime doneDateTime)
        {
            var noofWholeWeeks = (int)Math.Floor(days / 7);
            
            var noofWeekendDays = noofWholeWeeks * 2;

            if (!startDateTime.IsSameWeekAs(doneDateTime) && days < 7)
            {
                noofWeekendDays = 2;
            }

            return noofWeekendDays;
        }

        // TODO: where to put this logic?
        private string GetDefaultStoryPoint(string issueType)
        {
            decimal point;

            switch (issueType.ToLower())
            {
                case "defect":
                case "defect sub-task":
                    point = (decimal)0.5;
                    break;
                case "change request":
                    point = 1;
                    break;
                default:
                    return string.Empty;
            }

            return point.ToString("0.0", CultureInfo);
        }

        private static CultureInfo CultureInfo
        {
            get
            {
                var culture = CultureInfo.GetCultureInfo("sv-SE");
                return culture;
            }
        }

        public override string ToString()
        {
            var result = string.Format(
                CultureInfo,
                "{0}{4}{1}{4}{2:yyyy-MM-dd HH:mm:ss}{4}{3:yyyy-MM-dd HH:mm:ss}{4}{5:0.00}{4}{6}{4}{7}", 
                issue.Key, 
                issue.Fields.Summary, 
                StartDateTime, 
                DoneDateTime, 
                Separator, 
                CycleTime,
                StoryPoints,
                issue.Fields.EstimateInHours);

            return result;
        }

        private string ConvertNullableDateTimeToString(DateTime? date)
        {
            var formattedDate = date.HasValue ? date.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
            return formattedDate;
        }
    }
}