﻿using Newtonsoft.Json;

namespace HM.JiraMetrics.Lib.Jira.Model
{
    public class Fields
    {
        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }
        
        [JsonProperty("customfield_10093")]
        public decimal? StoryPoints { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("labels")]
        public string[] Labels { get; set; }

        [JsonProperty("issuetype")]
        public IssueType IssueType { get; set; }

        [JsonProperty("timeoriginalestimate")]
        public int? EstimateInSeconds { get; set; }

        public string EstimateInHours 
        {
            get
            {
                return EstimateInSeconds.HasValue ? string.Format("{0:0.##}", EstimateInSeconds / 3600) : string.Empty;
            }
        }
    }
}
