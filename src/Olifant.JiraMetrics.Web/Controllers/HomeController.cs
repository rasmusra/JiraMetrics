using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Web;
using System.Web.Mvc;

using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;

using Olifant.JiraMetrics.Lib;
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

        public ActionResult Index(string jql, string statuses)
        {
            var issues = String.IsNullOrEmpty(jql)
                ? new List<IIssueReportModel>()
                : GetIssues(jql, Status.CreateStatuses(statuses));

            var burnUpData = BurnUpGraph.SummonData(issues);

            var xaxisValues = new List<string> { "start" };
            xaxisValues.AddRange(burnUpData.Keys.Select(k => k.WeekLabel));
            var xaxis = new XAxis { Categories = xaxisValues.ToArray(), Title = new XAxisTitle { Text = "Week" } };

            var yaxisValues = new List<object> { "0" };
            yaxisValues.AddRange(burnUpData.Values.Select(v => v.StoryPoints).Cast<object>());
            var seriesColumn = new Series { Data = new Data(yaxisValues.ToArray()), Type = ChartTypes.Column };
            var seriesLine = new Series { Data = new Data(yaxisValues.ToArray()) };

            var yaxis = new YAxis { Min = 0, Title = new YAxisTitle { Text = "Story Points" } };

            var chart = new DotNet.Highcharts.Highcharts("chart")
                .SetXAxis(xaxis)
                .SetSeries(new[] { seriesLine, seriesColumn })
                .SetYAxis(yaxis);

            var title = new Title { Text = "Burnup" };
            chart.SetTitle(title);

            return View(chart);
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

        private IList<IIssueReportModel> GetIssues(string jql, Status[] cycleStatuses)
        {
            var jiraCyclesFacade = new JiraMetricsFacade(this.jiraClient);
            var filters = new List<IIssueFilter> { new WorkDoneFilter() };
            var cycleTimeRule = new CycleTimeRule(cycleStatuses);
            var result = jiraCyclesFacade.GetIssues(jql, cycleTimeRule, filters);
            return result;
        }
    }
}