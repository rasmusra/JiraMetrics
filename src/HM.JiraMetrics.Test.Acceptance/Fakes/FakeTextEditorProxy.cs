using System;
using System.Collections.Generic;
using System.Linq;

using HM.OT.JiraMetrics.Lib.Report;

namespace HM.OT.JiraMetrics.Test.Fakes
{
    internal class FakeTextEditorProxy : ITextEditorProxy
    {
        private string _text;

        public string ActualHeader
        {
            get
            {
                return ActualText.First();
            }
        }

        public string[] ActualRowsExclHeader
        {
            get
            {
                return ActualText
                       .Where(row => !string.IsNullOrWhiteSpace(row))
                       .Skip(1) // skip header
                       .ToArray();
            }
        }

        public IList<string> ActualText
        {
            get
            {
                return _text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            }
        }

        public void ShowInEditor(string rawText)
        {
            _text = rawText;
        }
    }
}