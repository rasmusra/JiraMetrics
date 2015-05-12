using System;
using System.Collections.Generic;
using System.Linq;
using Olifant.JiraMetrics.Lib.Metrics.Model;

namespace Olifant.JiraMetrics.Lib.Metrics.BurnUp
{

    // TODO: it is very hard wired that graph shows weekly iterations...
    public class BurnUpGraph
    {
        private readonly Dictionary<BurnUpGraphWeek, BurnUpGraphThroughput> _weekToThroughputDict = new Dictionary<BurnUpGraphWeek, BurnUpGraphThroughput>();

        public BurnUpGraph(ICollection<IIssueReportModel> issueReportModels)
        {
            SummonThroughput(issueReportModels);
        }

        /// <summary>
        /// Returns list with weekly aggregated values; that is, aggregate on all earlier weeks 
        /// </summary>
        public IList<decimal> AccumulatedPointsList
        {
            get
            {
                if (!Weeks.Any())
                {
                    return new List<decimal>();
                }

                return Weeks.First().To(Weeks.Last())
                    .Select(week => Weeks.First().To(week).ToList())
                    .Select(allEarlierWeeks => allEarlierWeeks
                        .Sum(earlierWeek => _weekToThroughputDict[earlierWeek].StoryPoints))
                        .ToList();
            }
        }

        public List<BurnUpGraphWeek> Weeks { get { return _weekToThroughputDict.Keys.ToList(); } }

        private void SummonThroughput(ICollection<IIssueReportModel> issueReportModels)
        {
            if (issueReportModels.Count == 0)
            {
                return;
            }

            var firstWeek = new BurnUpGraphWeek(issueReportModels.Min(irm => irm.DoneDateTime));
            var lastWeek = new BurnUpGraphWeek(issueReportModels.Max(irm => irm.DoneDateTime));

            foreach (var week in firstWeek.To(lastWeek))
            {
                var matchingIssues = issueReportModels
                    .Where(irm => week.Contains(DateTime.Parse(irm.DoneDateTime)))
                    .ToList();

                _weekToThroughputDict[week] = new BurnUpGraphThroughput(matchingIssues);
            }
        }
    }
}
