using System;
using System.Linq;

using Newtonsoft.Json;

namespace Olifant.JiraMetrics.Lib.Jira.Model
{
    public class HistoryItem
    {
        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("fromString")]
        public string FromValue { get; set; }

        [JsonProperty("toString")]
        public string ToValue { get; set; }

        public bool WasLabelledAsStartedItem(string startedLabel)
        {
            return Field == "labels" && FromValue.ToLower().Contains(startedLabel.ToLower());
        }

        public bool IsChangedToStartedItem(string startedLabel)
        {
            return LabelIsAdded(startedLabel);
        }

        public bool LabelIsAdded(string value)
        {
            // for labels it is ok to use "Contains" because they never have spaces in their names, instead they are separated with spaces
            var result = Field == "labels" && !FromValue.ToLower().Contains(value) && ToValue.ToLower().Contains(value);
            return result;
        }

        public bool AnyStatusIsRemoved(params Status[] values)
        {
            return values.Any(value => ValueIsRemoved("status", value.Name));
        }

        public bool AnyStatusIsAdded(params Status[] values)
        {
            return values.Any(value => ValueIsAdded("status", value.Name));
        }

        private bool ValueIsRemoved(string changedField, string value)
        {
            if (Field != changedField)
            {
                return false;
            }

            var result = FromValue.Equals(value, StringComparison.OrdinalIgnoreCase) && !ToValue.Equals(value, StringComparison.OrdinalIgnoreCase);

            return result;
        }

        private bool ValueIsAdded(string changedField, string value)
        {
            if (Field != changedField)
            {
                return false;
            }

            var result = !FromValue.Equals(value, StringComparison.OrdinalIgnoreCase) && ToValue.Equals(value, StringComparison.OrdinalIgnoreCase);

            return result;
        }
    }
}
