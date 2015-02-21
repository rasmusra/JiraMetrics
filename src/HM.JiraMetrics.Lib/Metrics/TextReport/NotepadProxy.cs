using System;
using System.Diagnostics;
using System.IO;

namespace HM.JiraMetrics.Lib.Metrics.TextReport
{
    public class NotepadProxy : ITextEditorProxy
    {
        public const string ReportSeparator = "¤";

        public void ShowInEditor(string text)
        {
            var filename = Path.Combine(
                @"C:\Windows\Temp", 
                string.Format("jira_export_{0}.txt", DateTime.Now.ToString("yyyyMMdd_HHmmss")));

            File.WriteAllText(filename, text);

            Process.Start(filename);
        }
    }
}
