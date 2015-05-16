using System;
using FluentAssertions;
using NUnit.Framework;
using Olifant.JiraMetrics.Lib.Jira;

namespace Olifant.JiraMetrics.Test.Unit
{
    class JiraProjectQueryTests
    {
        [TestCase("DISCO", "2015-05-15")]
        [TestCase("nisse", "1999-01-01")]
        public void ProvidesJqlQueryWithUpdatedFilter(string projectName, string updatedDate)
        {
            var target = new JiraProjectQuery(projectName, DateTime.Parse(updatedDate));
            target.JqlQuery.Should().Contain(projectName);
            target.JqlQuery.Should().Contain(updatedDate);
        }
    }
}
