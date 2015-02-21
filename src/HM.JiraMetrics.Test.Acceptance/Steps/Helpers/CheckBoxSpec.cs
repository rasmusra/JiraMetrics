using System.Collections.Generic;
using System.Diagnostics;

namespace HM.JiraMetrics.Test.Acceptance.Steps.Helpers
{
    [DebuggerDisplay("{CheckboxName} = {IsTrue}")]
    public class CheckBoxSpec
    {
        private readonly List<string> checkedLabels = new List<string> { "checked", "check", "yes" };

        public bool IsTrue
        {
            get
            {
                return this.checkedLabels.Contains(this.IsChecked);
            }
        }

        public string IsChecked
        {
            get;
            set;
        }

        public string CheckboxName
        {
            get;
            set;
        }
    }
}