using System;
using System.Collections.Generic;
using System.Linq;

using HM.JiraMetrics.Lib.Metrics.TextReport;

namespace HM.JiraMetrics.Test.Fakes
{
    public class FakeTextEditorProxy : ITextEditorProxy
    {
        private string text;

        public string ActualHeader
        {
            get
            {
                return this.ActualText.First();
            }
        }

        public string[] ActualRowsExclHeader
        {
            get
            {
                return this.ActualText
                       .Where(row => !string.IsNullOrWhiteSpace(row))
                       .Skip(1) // skip header
                       .ToArray();
            }
        }

        public IList<string> ActualText
        {
            get
            {
                return this.text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            }
        }

        public void ShowInEditor(string rawText)
        {
            this.text = rawText;
        }
    }
}