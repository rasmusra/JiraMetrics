using System;
using System.Collections.Generic;
using System.Linq;

namespace Olifant.JiraMetrics.Lib.Jira
{
    public static class JqlChunkHelper
    {
        public static string AppendChunkFilter(string jql, DateTime startDate, int chunkSizeInDays)
        {
            const string OrderByFlag = "order by";
            var endDate = startDate.AddDays(chunkSizeInDays);
            var jqlParts = jql.ToLower().Split(new[] { "order by" }, StringSplitOptions.None);

            var jqlChunkFilter = string.Format(
                "({0}) AND createdDate >= {1:yyyy-MM-dd} AND createdDate < {2:yyyy-MM-dd} {3}",
                jqlParts[0].Trim(),
                startDate,
                endDate,
                jqlParts.Length > 1 ? string.Format("{0}{1}", OrderByFlag, jqlParts[1]) : string.Empty);

            return jqlChunkFilter;
        }

        public static string SetToOrderByCreadedDate(string jql)
        {
            var jqlParts = jql.ToLower().Split(new[] { "order by" }, StringSplitOptions.None);
            var result = string.Format("{0} ORDER BY CreatedDate ASC", jqlParts[0]);
            return result;
        }

        /// <summary>
        /// We are hereby assuming that jira-json is well-formatted with tag
        ///     "created":"[datetime]"
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static List<DateTime?> GetCreatedDates(string json)
        {
            const string CreatedDateKey = @"""created"":""";

            var createdDates =
                json.Split(new[] { CreatedDateKey }, StringSplitOptions.None)
                    .Skip(1)
                    .Select(s => s.Substring(0, s.IndexOf(@"""")))
                    .Select(DateTime.Parse)
                    .Select(dt => (DateTime?)dt)
                    .ToList();

            return createdDates;
        }
    }
}