using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Olifant.JiraMetrics.Lib.Jira.Model
{
    [DebuggerDisplay("{Name}")]
    public class Status 
    {
        private static readonly Status[] OpenStatuses = new Status[] { new Status { Name = "open" }, new Status { Name = "reopened" } };

        [JsonProperty("name")]
        public string Name { get; set; }

        public bool IsOpen
        {
            get
            {
                return OpenStatuses.Any(this.Equals);
            }    
        }

        public static Status[] Create(params string[] statusNames)
        {
            return statusNames==null
            ? new Status[]{}
            : statusNames.Select(name => new Status { Name = name }).ToArray();
        }

        /// <summary>
        /// Deserialize a comma separated list of statuses
        /// </summary>
        /// <param name="statuses"></param>
        /// <returns></returns>
        public static Status[] Deserialize(string statuses)
        {
            if (string.IsNullOrEmpty(statuses))
                return new Status[]{};

            var deserializedStatuses = statuses
                .Split(',')
                .Select(s => s.Trim())
                .Select(name => new Status { Name = name })
                .ToArray();
            return deserializedStatuses;
        }

        public override bool Equals(object other)
        {
            return String.Equals(this.Name, ((Status)other).Name, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return this.Name != null ? this.Name.ToLower().GetHashCode() : 0;
        }
    }
}
