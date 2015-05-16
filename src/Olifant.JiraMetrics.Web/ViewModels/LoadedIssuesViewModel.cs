using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Olifant.JiraMetrics.Web.Models;

namespace Olifant.JiraMetrics.Web.ViewModels
{
    public class LoadedIssuesViewModel
    {
        public IEnumerable<LoadedIssue> LoadedIssues { get; set; }
        public string ProjectName { get; set; }
    }
}