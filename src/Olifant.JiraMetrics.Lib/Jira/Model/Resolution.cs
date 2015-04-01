using Newtonsoft.Json;

namespace Olifant.JiraMetrics.Lib.Jira.Model
{
    public class Resolution
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
