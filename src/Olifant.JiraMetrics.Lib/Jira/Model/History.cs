using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace Olifant.JiraMetrics.Lib.Jira.Model
{
    public class History
    {
        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("items")]
        public List<HistoryItem> HistoryItems { get; set; }

        public bool IsLabelHistory
        {
            get
            {
                return HistoryItems.Exists(item => item.Field == "labels");
            }
        }

        public bool IsChangedToStarted(string startedLabel)
        {
            return HistoryItems.Exists(item => item.IsChangedToStartedItem(startedLabel));
        }

        public bool IsLabelledAsStartedAtCreation(string startedLabel)
        {
            return HistoryItems.Exists(item => item.WasLabelledAsStartedItem(startedLabel));
        }

        public bool IsMovedToAnyOf(params Status[] statuses)
        {
            var statusChangingHistoryItems = HistoryItems.Where(item => item.Field == "status").ToList();

            var res =
                statusChangingHistoryItems.Exists(
                    item => item.AnyStatusIsAdded(statuses) && !item.AnyStatusIsRemoved(statuses));
            
            return res;
        }

        public bool IsMovedFromAnyOf(params Status[] statuses)
        {
            var statusChangingHistoryItems = HistoryItems.Where(item => item.Field == "status").ToList();

            var res =
                statusChangingHistoryItems.Exists(
                    item => item.AnyStatusIsRemoved(statuses) && !item.AnyStatusIsAdded(statuses));
            return res;
        }
    }
}
