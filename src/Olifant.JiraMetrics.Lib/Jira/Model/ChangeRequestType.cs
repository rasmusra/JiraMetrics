using Newtonsoft.Json;

namespace Olifant.JiraMetrics.Lib.Jira.Model
{
    public class ChangeRequestType
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
