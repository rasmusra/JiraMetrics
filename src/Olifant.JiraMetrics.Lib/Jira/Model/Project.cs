using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Olifant.JiraMetrics.Lib.Jira.Model
{
    [DebuggerDisplay("{Name}")]
    public class Project 
    {
        [JsonProperty("name"), BsonElement("name")]
        public string Name { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }
    }
}
