using System.Collections.Generic;
using System.Linq;

namespace Olifant.JiraMetrics.Test.Acceptance.Steps.Specs
{
    public class BurnUpGraphSpec
    {
        public string StartX { get; set; }
        public string EndX { get; set; }
        public string StartY { get; set; }
        public string EndY { get; set; }
        public string YValues { get; set; }

        public IList<string> Fields
        {
            get
            {
                var values = YValuesList;
                values.Add(StartX ?? "");
                values.Add(EndX ?? "");

                return values;
            }
        }

        /// <summary>
        /// This one returns some text for y-axis values that should be found in html chart data.
        /// For example, if we expect y-values
        ///     0, 1, 8
        /// then we know (as programmers) that html will contain y-values along x-axis maybe like this:
        ///     "data: [0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 8]"
        /// and we can (and will) settle with expectig the following texts 
        ///     "0, 1"
        ///     "1, 8"
        /// 
        /// We might need to make this function more (or less!) clever as new unexpected 
        /// behavours comes up, not detected from this spec.
        /// </summary>
        private IList<string> YValuesList
        {
            get
            {
                if (string.IsNullOrEmpty(YValues))
                    return new List<string>();

                var sortedYValues = YValues.Split(',')
                    .OrderBy(decimal.Parse)
                    .Select(s => s.Trim())
                    .ToList();

                var result = new List<string>();

                // select pairs of values
                for (var i = 0; i < sortedYValues.Count()-1; i++)
                {
                    result.Add(string.Format("{0}, {1}", sortedYValues.ElementAt(i), sortedYValues.ElementAt(i + 1)));
                }

                return result;
            }
        }
    }
}