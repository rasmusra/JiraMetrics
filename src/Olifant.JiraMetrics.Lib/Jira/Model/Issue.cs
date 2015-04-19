using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace Olifant.JiraMetrics.Lib.Jira.Model
{
    using Newtonsoft.Json;

    public class Issue
    {
        [JsonProperty("key"), BsonId]
        public string Key { get; set; }

        [JsonProperty("fields"), BsonElement("fields")]
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

        public IList<Issue> CloneMany(int noofClones)
        {
            var json = JsonConvert.SerializeObject(this);
            var result = new List<Issue>(noofClones);

            for (var i = 0; i < noofClones; i++)
            {
                var newKey = string.Format("{0}-{1}", Key, i);
                var clonedJson = json.Replace(Key, newKey);
                var clonedIssue = JsonConvert.DeserializeObject<Issue>(clonedJson);
                result.Add(clonedIssue);
            }

            return result;
        }
    }
}
