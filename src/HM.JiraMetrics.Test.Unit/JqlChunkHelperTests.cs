using System;
using System.Linq;

using FluentAssertions;

using HM.JiraMetrics.Lib.Jira;

using NUnit.Framework;

namespace HM.JiraMetrics.Test.Unit
{
    public class JqlChunkHelperTests
    {
        [Test]
        public void SuppliesJqlWithChunkFilter()
        {
            const string Jql = "project in (ofu,scsc,disco)";
            var startDate = new DateTime(2010, 1, 1);
            const int ChunkSize = 7;
            var endDate = startDate.AddDays(ChunkSize);

            var actual = JqlChunkHelper.AppendChunkFilter(Jql, startDate, ChunkSize);

            actual.Should().Contain(string.Format("createdDate >= {0:yyyy-MM-dd}", startDate));
            actual.Should().Contain(string.Format("createdDate < {0:yyyy-MM-dd}", endDate));
        }

        [Test]
        public void WrapsOriginalJqlInParenthesis()
        {
            // arrange
            const string Jql = "project in (ofu,scsc,disco)";
            var startDate = new DateTime(2010, 1, 1);
            const int ChunkSize = 7;

            // act
            var actual = JqlChunkHelper.AppendChunkFilter(Jql, startDate, ChunkSize);

            // assert
            actual.Should().Contain(string.Format("({0})", Jql));
        }

        [Test]
        public void OrderByClauseIsAtEndOfJql()
        {
            // arrange
            const string OrderClause = "order by nisse";
            string jql = string.Format("project in (ofu,scsc,disco) {0}", OrderClause);
            var startDate = new DateTime(2010, 1, 1);
            const int ChunkSize = 7;

            // act
            var actual = JqlChunkHelper.AppendChunkFilter(jql, startDate, ChunkSize);

            // assert
            actual.Should().EndWith(OrderClause);
        }

        [Test]
        public void ProvidesListOfAllCreatedDates()
        {
            // arrange
            var firstDate = new DateTime(2014, 4, 15);
            var lastDate = new DateTime(2014, 4, 16);
            var givenJson = @"{""created"":""" + firstDate
                            + @""",""created"":""2014-05-01"",""otherDate"":""2014-04-01"",""someotherstuff"":""sdd"",""created"":"""
                            + lastDate + @"""}";

            // act
            var actual = JqlChunkHelper.GetCreatedDates(givenJson);

            // assert
            actual.First().ShouldBeEquivalentTo(firstDate);
            actual.Last().ShouldBeEquivalentTo(lastDate);
        }

        [Test]
        public void CreateDatesIsNullableDateTimes()
        {
            // arrange
            const string GivenJson = @"""someotherstuff"":""sdd""";

            // act
            var actual = JqlChunkHelper.GetCreatedDates(GivenJson);

            // assert
            actual.FirstOrDefault().ShouldBeEquivalentTo(null);
        }
    }
}
