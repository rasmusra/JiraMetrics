using System.Collections.Generic;

using Newtonsoft.Json;

namespace Olifant.JiraMetrics.Lib.Jira.Model
{
    public class Issues
    {
        [JsonProperty("issues")]
        public List<Issue> IssueList { get; set; }
    }
}
