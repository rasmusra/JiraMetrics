using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Olifant.JiraMetrics.Test.Unit
{
    class JiraProjectQueryTests
    {
        [TestCase("DISCO")]
        [TestCase("nisse")]
        public void ProvidesJqlQuery(string projectName)
        {
            var target = new JiraProjectQuery(projectName);
            var expected = string.Format("project='{0}'", projectName);

            target.JqlQuery.ShouldBeEquivalentTo(expected);
        }
    }
}
