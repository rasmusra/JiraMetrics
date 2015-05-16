using System;

namespace Olifant.JiraMetrics.Lib.Jira
{
    public class JiraProjectQuery
    {
        public JiraProjectQuery(string projectName, DateTime updateDate)
        {
            ProjectName = projectName;
            UpdateDate = updateDate;
        }

        public string ProjectName { get; private set; }
        public DateTime UpdateDate { get; set; }

        public string JqlQuery { get { return string.Format("project='{0}' AND updatedDate > '{1:yyyy-MM-dd}'", ProjectName, UpdateDate); } }
    }
}