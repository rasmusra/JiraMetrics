using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using Olifant.JiraMetrics.Lib.Metrics.BurnUp;

namespace Olifant.JiraMetrics.Web.Models
{
    public class BurnUpViewModel
    {
        public BurnUpViewModel(Dictionary<BurnUpWeekIteration, BurnUpValue> burnUpData)
        {
            Chart = CreateChart(burnUpData);
        }

        [DisplayName("Chart")]
        public Highcharts Chart { get; set; }

        private Highcharts CreateChart(Dictionary<BurnUpWeekIteration, BurnUpValue> burnUpData)
        {
            var xaxis = CreateXaxis(burnUpData);
            var yaxisValues = CreateYAxis(burnUpData);
            var yaxis = new YAxis
            {
                Min = 0, 
                Title = new YAxisTitle { Text = "Story Points" },
            };

            var seriesColumn = new Series { Data = new Data(yaxisValues.ToArray()), Type = ChartTypes.Column };
            var seriesLine = new Series { Data = new Data(yaxisValues.ToArray()) };

            var chart = new Highcharts("chart")
                .SetXAxis(xaxis)
                .SetSeries(new[] { seriesLine, seriesColumn })
                .SetYAxis(yaxis)
                .SetTooltip(new Tooltip(){Formatter = @"function() { return  ''+ this.x +': '+ this.y +' accumulated points'; }" });

            var title = new Title { Text = "Burnup" };
            chart.SetTitle(title);
            return chart;
        }

        private static List<object> CreateYAxis(Dictionary<BurnUpWeekIteration, BurnUpValue> burnUpData)
        {
            var yaxisValues = new List<object> {"0"};
            yaxisValues.AddRange(burnUpData.Values
                .Select(v => v.StoryPoints)
                .Cast<object>());
;

            return yaxisValues;
        }

        private static XAxis CreateXaxis(Dictionary<BurnUpWeekIteration, BurnUpValue> burnUpData)
        {
            var xaxisValues = new List<string> {"start"};
            xaxisValues.AddRange(burnUpData.Keys.Select(k => k.WeekLabel));
            var xaxis = new XAxis {Categories = xaxisValues.ToArray(), Title = new XAxisTitle {Text = "Week"}};
            return xaxis;
        }
    }
}