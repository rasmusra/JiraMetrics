using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

namespace HM.JiraMetrics.Lib.Jira.Model
{
    public class ChangeLog
    {
        [JsonProperty("histories")]
        public List<History> Histories { get; set; }

        public DateTime? GetDateWhenItemIsLabelledAsStarted(string issueCreationDate, string startedLabel)
        {
            if (startedLabel == null)
            {
                return null;
            }

            var firstLabelHistoryItem = Histories.FirstOrDefault(history => history.IsLabelHistory);

            if (firstLabelHistoryItem != null && firstLabelHistoryItem.IsLabelledAsStartedAtCreation(startedLabel))
            {
                // this means that the started label was set when the issue was created
                return DateTime.Parse(issueCreationDate);
            }

            var firstOrDefault = Histories.FirstOrDefault(h => h.IsChangedToStarted(startedLabel));
            if (firstOrDefault != null)
            {
                // this means that the started-date is to be found in the changelog where started-label was added
                return DateTime.Parse(firstOrDefault.Created);
            }

            return null;
        }

        public History FirstHistoryMovedToStatus(params Status[] statuses)
        {
            return Histories.FirstOrDefault(
                h =>
                    {
                        var res = h.IsMovedToAnyOf(statuses);
                        return res;
                    });
        }

        public History LastHistoryMovedFromStatus(params Status[] statuses)
        {
            return Histories.LastOrDefault(
                h =>
                    {
                        var res = h.IsMovedFromAnyOf(statuses);
                        return res;
                    });
        }
    }
}
