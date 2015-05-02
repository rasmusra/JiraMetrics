using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Olifant.JiraMetrics.Lib.Jira.Model;

namespace Olifant.JiraMetrics.Web.Models
{
    public class ProjectViewModel
    {
        public ProjectViewModel(List<Project> projects)
        {
            ProjectList = projects.Select(p => 
                new SelectListItem
                {
                    Text = p.Name, Value = projects.IndexOf(p).ToString() 
                
                }).ToList();
        }

        [DisplayName("Projects")]
        public List<SelectListItem> ProjectList { get; set; }
    }
}