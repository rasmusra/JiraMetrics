using Newtonsoft.Json;

namespace Olifant.JiraMetrics.Lib.Jira.Model
{
    public class IssueType
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("subtask")]
        public bool IsSubtask { get; set; }
    }
}
