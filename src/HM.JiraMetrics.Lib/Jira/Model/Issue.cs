using System;
using System.Linq;

namespace HM.JiraMetrics.Lib.Jira.Model
{
    using Newtonsoft.Json;

    public class Issue
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("fields")]
        public Fields Fields { get; set; }

        [JsonProperty("changelog")]
        public ChangeLog ChangeLog { get; set; }

        public DateTime? GetDateWhenItemWasLabelledAsStarted(string startedLabel)
        {
            if (string.IsNullOrEmpty(startedLabel))
            {
                return null;
            }

            var firstLabelHistoryItem = ChangeLog.Histories.FirstOrDefault(history => history.IsLabelHistory);

            if (firstLabelHistoryItem != null && firstLabelHistoryItem.IsLabelledAsStartedAtCreation(startedLabel))
            {
                // this means that the started label was set when the issue was created
                return DateTime.Parse(Fields.Created);
            }

            var firstOrDefault = ChangeLog.Histories.FirstOrDefault(h => h.IsChangedToStarted(startedLabel));
            if (firstOrDefault != null)
            {
                // this means that the started-date is to be found in the changelog where started-label was added
                return DateTime.Parse(firstOrDefault.Created);
            }

            return null;
        }

        public bool HasLabel(string label)
        {
            return
                Fields.Labels.Any(
                    existingLabel => string.Equals(existingLabel, label, StringComparison.OrdinalIgnoreCase));
        }
    }
}
