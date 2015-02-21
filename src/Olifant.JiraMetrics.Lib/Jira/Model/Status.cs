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

        public static Status[] CreateStatuses(params string[] statusNames)
        {
            return statusNames.Select(name => new Status { Name = name }).ToArray();
        }

        public override bool Equals(object other)
        {
            return string.Equals(this.Name, ((Status)other).Name, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return this.Name != null ? this.Name.ToLower().GetHashCode() : 0;
        }
    }
}
