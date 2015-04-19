using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MongoDB.Driver.Linq;
using Olifant.JiraMetrics.Lib.Data;
using Olifant.JiraMetrics.Lib.Jira;
using Olifant.JiraMetrics.Lib.Jira.Model;
using Olifant.JiraMetrics.Lib.Metrics;
using Olifant.JiraMetrics.Lib.Metrics.BurnUp;
using Olifant.JiraMetrics.Lib.Metrics.Filters;
using Olifant.JiraMetrics.Lib.Metrics.Model;
using Olifant.JiraMetrics.Web.Models;

namespace Olifant.JiraMetrics.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJiraRestClient jiraClient;

        public HomeController(IJiraRestClient jiraClient)
        {
            this.jiraClient = jiraClient;
        }

        public ActionResult Index(string project, string statuses)
        {
            var deserializedStatuses = Status.Deserialize(statuses);

            var issues = String.IsNullOrEmpty(project)
                ? new List<IIssueReportModel>()
                : GetIssues(project, deserializedStatuses);

            var burnUpData = BurnUpGraph.SummonData(issues);
            var burnUpModel = new BurnUpViewModel(burnUpData);

            return View(burnUpModel);
        }

        [ChildActionOnly]
        public PartialViewResult Projects()
        {
            var mongoAccess = new MongoAccess(ConfigurationManager.AppSettings["ConnectionString"]);
            var projects = (from i in mongoAccess.GetCollection<Issue>().FindAll()
                            select i.Fields.Project)
                            .DistinctBy(p => p.Name)
                            .ToList();
            var vm = new ProjectViewModel(projects);
            return PartialView(vm);
        }

        [ChildActionOnly]
        public PartialViewResult Cycles()
        {
            var cyclesViewModel = new CyclesViewModel
                                      {
                                          PreCycleStatuses = new List<string> { "Open", "Reopened" },
                                          CycleStatuses = new List<string>
                                                              {
                                                                  "System Test",           
                                                                  "Ready for Test",        
                                                                  "Describing Requirement",
                                                                  "Build & Configure",     
                                                                  "Building & Configuring",
                                                                  "System Testing",        
                                                                  "Describe Requirement",  
                                                                  "Review"
                                                              },
                                          PostCycleStatuses = new List<string>
                                                                  {
                                                                      "Deployed In Acceptance Test",
                                                                      "System Integration Test",
                                                                      "System Integration Testing",
                                                                      "Resolved",
                                                                      "Closed",
                                                                      "Acceptance Test",
                                                                      "Acceptance Testing",
                                                                      "System Test Done",
                                                                      "Design Architecture",
                                                                      "Designing Architecture",
                                                                      "Implement",
                                                                      "Implementing",
                                                                      "Test",
                                                                      "Testing",
                                                                      "Deployed In Test",
                                                                  }
                                      };

            return PartialView(cyclesViewModel);
        }

        private IList<IIssueReportModel> GetIssues(string project, Status[] cycleStatuses)
        {
            var filters = new List<IIssueFilter> { new WorkDoneFilter() };
            var cycleTimeRule = new CycleTimeRule(cycleStatuses);

            var mongoAccess = new MongoAccess(ConfigurationManager.AppSettings["ConnectionString"]);
            var q = from issue in mongoAccess.GetCollection<Issue>().AsQueryable<Issue>()
                    where issue.Fields.Project.Name == project 
                    select issue;
            var mongoQuery = ((MongoQueryable<Issue>)q).GetMongoQuery();
   
            var issues = mongoAccess.GetCollection<Issue>().Find(mongoQuery).ToList();
            var reportItemModels = IssueReportModelFactory.Create(issues, cycleTimeRule);

            var result = reportItemModels
                .Where(ri => filters.All(f => f.IsOk(ri)))
                .OrderBy(ri => ri.IssueType)
                .ToList();

            return result;
        }
    }
}