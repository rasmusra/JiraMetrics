using System.Collections.Generic;
using System.Linq;

using Olifant.JiraMetrics.Lib.Metrics.Model;

namespace Olifant.JiraMetrics.Lib.Metrics.BurnUp
{
    public class BurnUpGraph
    {
        public static Dictionary<BurnUpWeekIteration, BurnUpValue> SummonData(IList<IIssueReportModel> issueReportModels)
        {
            if (issueReportModels.Count == 0)
            {
                return new Dictionary<BurnUpWeekIteration, BurnUpValue>();
            }

            var result = new Dictionary<BurnUpWeekIteration, BurnUpValue>();

            // create all burnup-iterations
            var orderedIterations = issueReportModels
                .Select(irm => new BurnUpWeekIteration(irm.DoneDateTime))
                .OrderBy(i => i.WeekLabel);

            var iter = orderedIterations.First();
            decimal accumulatedPoints = 0;

            while (iter.CompareTo(orderedIterations.Last()) <= 0)
            {
                var matchingIssues = issueReportModels
                    .Where(irm => iter.CompareTo(new BurnUpWeekIteration(irm.DoneDateTime)) == 0)
                    .ToList();

                var totalPointsInIteration = matchingIssues
                    .Sum(i => decimal.Parse(i.StoryPoints));

                accumulatedPoints += totalPointsInIteration;
                result[iter] = new BurnUpValue(accumulatedPoints);
                iter = iter.AddWeek();
            }

            return result;
        }
    }
}
