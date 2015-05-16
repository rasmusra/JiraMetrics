using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using Newtonsoft.Json;
using Olifant.JiraMetrics.Lib.Data;
using Olifant.JiraMetrics.Lib.Jira;
using Olifant.JiraMetrics.Lib.Jira.Model;
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
            var mongoAccess = new MongoAccess(ConfigurationManager.AppSettings["ConnectionString"]);

            // get updateddate from mongo
            var latestUpdatedDateString = mongoAccess.GetCollection<Issue>()
                .AsQueryable()
                .Max(issue => issue.Fields.Updated);

            // load from jira
            var projectQuery = new JiraProjectQuery(project, DateTime.Parse(latestUpdatedDateString));
            var jsonChunks = jiraClient.GetJsonChunks(projectQuery);
            var updatedIssues = jsonChunks.SelectMany(chunk =>
            {
                var chunkOfIssues = JsonConvert.DeserializeObject<Issues>(chunk);
                return chunkOfIssues.IssueList;
            }).ToList();

            // upsert to mongo
            foreach (var issue in updatedIssues)
            {
                mongoAccess.GetCollection<Issue>().Update(
                    Query.EQ("Key", issue.Key),
                    Update.Replace(issue),
                    UpdateFlags.Upsert); 
            }

            // respond with log
            var vm = new LoadedIssuesViewModel()
            {
                LoadedIssues = updatedIssues.Select(issue => new LoadedIssue { Action = "Added", Key = issue.Key }),
                ProjectName = project
            };

            return PartialView("LoadedIssueListControl", vm);
        }
    }
}