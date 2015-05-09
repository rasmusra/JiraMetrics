using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver.Linq;
using Newtonsoft.Json;
using Olifant.JiraMetrics.Lib.Data;
using Olifant.JiraMetrics.Lib.Jira;
using Olifant.JiraMetrics.Lib.Jira.Model;
using Olifant.JiraMetrics.Lib.Metrics;
using Olifant.JiraMetrics.Lib.Metrics.Filters;
using Olifant.JiraMetrics.Lib.Metrics.Model;
using Olifant.JiraMetrics.Web.Models;
using Olifant.JiraMetrics.Web.ViewModels;

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

        public ActionResult LoadIssues(string project)
        {
            // load from jira
            var jsonChunks = jiraClient.GetJsonChunks(new JiraProjectQuery(project));
            var issues = jsonChunks.SelectMany(chunk =>
            {
                var chunkOfIssues = JsonConvert.DeserializeObject<Issues>(chunk);
                return chunkOfIssues.IssueList;
            }).ToList();

            // store to mongo
            // TODO: move to repo
            var mongoAccess = new MongoAccess(ConfigurationManager.AppSettings["ConnectionString"]);
            mongoAccess.GetCollection<Issue>().Drop();
            mongoAccess.GetCollection<Issue>().InsertBatch(issues);

            // respond with log
            var vm = new LoadedIssuesViewModel
            {
                LoadedIssues = issues.Select(issue => new LoadedIssue {Action = "Added", Key = issue.Key})
            };

            return PartialView("LoadedIssueListControl", vm);
        }
    }
}