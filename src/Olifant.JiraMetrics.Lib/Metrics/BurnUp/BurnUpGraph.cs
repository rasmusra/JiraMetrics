using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
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

            var orderedIterations = issueReportModels
                .Select(irm => new BurnUpGraphWeek(irm.DoneDateTime))
                .OrderBy(i => i.WeekLabel);

            foreach (var week in orderedIterations.First().To(orderedIterations.Last()))
            {
                var matchingIssues = issueReportModels
                    .Where(irm => week.CompareTo(new BurnUpGraphWeek(irm.DoneDateTime)) == 0)
                    .ToList();

                _weekToThroughputDict[week] = new BurnUpGraphThroughput(matchingIssues);
            }
        }
    }
}
