﻿using System.Collections.Generic;
using System.Linq;

using HM.JiraMetrics.Lib.Metrics.TextReport;
using HM.JiraMetrics.WinApp.Properties;

namespace HM.JiraMetrics.WinApp
{
    internal class JqlQueryManager
    {
        public IEnumerable<string> JqlQueries
        {
            get
            {
                return new List<string>(Settings.Default.JqlQueries.Split(new[] { NotepadProxy.ReportSeparator[0] }));
            }
            set
            {
                Settings.Default.JqlQueries = string.Join(NotepadProxy.ReportSeparator, value.ToArray());
                Settings.Default.Save();
            }
        }

        public void SaveQuery(string newJql)
        {
            var queries = JqlQueries.ToList();

            if (!queries.Contains(newJql))
            {
                queries.Add(newJql);
            }

            JqlQueries = queries;
        }
    }
}
