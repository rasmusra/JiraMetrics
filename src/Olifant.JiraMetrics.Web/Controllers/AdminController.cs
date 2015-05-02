using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Olifant.JiraMetrics.Lib.Jira;
using Olifant.JiraMetrics.Lib.Jira.Model;
using Olifant.JiraMetrics.Lib.Metrics;
using Olifant.JiraMetrics.Lib.Metrics.Filters;
using Olifant.JiraMetrics.Lib.Metrics.Model;

namespace Olifant.JiraMetrics.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IJiraRestClient jiraClient;

        public AdminController(IJiraRestClient jiraClient)
        {
            this.jiraClient = jiraClient;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin
        public ActionResult Load(string project)
        {
            // get all issues from jira for given project
            var issues = new List<Issue>();
            var jql = string.Format("Project='{0}'", project);
            var jsonChunks = jiraClient.GetJsonChunks(jql);

            jsonChunks.ForEach(
                chunk =>
                {
                    var chunkOfIssues = JsonConvert.DeserializeObject<Issues>(chunk);
                    issues.AddRange(chunkOfIssues.IssueList);
                });

            // generate load report
            return PartialView(null);
        }
    }
}