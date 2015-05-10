using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Olifant.JiraMetrics.Lib.Metrics.BurnUp
{
    [DebuggerDisplay("{WeekLabel}")]
    public class BurnUpGraphWeek : IComparable<BurnUpGraphWeek>, IEquatable<BurnUpGraphWeek>
    {
        public readonly int Week;
        public readonly int Year;

        public BurnUpGraphWeek(string date)
            : this(DateTime.Parse(date))
        {
        }

        public BurnUpGraphWeek(DateTime date)
        {
            this.Week = date.GetIso8601WeekOfYear();
            this.Year = date.Year;

            // this happens when we are in week 53, we convert 53 to 1 
            if (this.Week == 1 && date.Month == 12)
            {
                this.Year += 1;
            }
        }

        public string WeekLabel
        {
            get { return string.Format("y{0}w{1}", this.Year%100, this.Week); }
        }

        public override int GetHashCode()
        {
            return this.WeekLabel.GetHashCode();
        }

        public int CompareTo(BurnUpGraphWeek other)
        {
            return string.Compare(this.WeekLabel, other.WeekLabel, StringComparison.Ordinal);
        }

        public BurnUpGraphWeek AddWeek()
        {
            var date = FirstDateOfWeekIso8601(this.Year, this.Week);
            return new BurnUpGraphWeek(date.AddDays(7));
        }

        private static DateTime FirstDateOfWeekIso8601(int year, int weekOfYear)
        {
            var jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }

            var result = firstThursday.AddDays(weekNum*7);
            return result.AddDays(-3);
        }

        public IEnumerable<BurnUpGraphWeek> To(BurnUpGraphWeek endWeek)
        {
            for (var week = this; week.CompareTo(endWeek) <= 0; week = week.AddWeek())
            {
                yield return week;
            }
        }

        public bool Equals(BurnUpGraphWeek other)
        {
            return this.WeekLabel == other.WeekLabel;
        }
    }
}
