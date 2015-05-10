using FluentAssertions;
using NUnit.Framework;
using Olifant.JiraMetrics.Lib.Jira;

namespace Olifant.JiraMetrics.Test.Unit
{
    public class JiraCredentialsTests
    {
        [Test]
        public void ProvidesEncryptedUserCredentialsOnLoginFormat()
        {
            const string GivenName = "nisse";
            const string GivenPassword = "essin";
            var target = new JiraCredentials(GivenName, GivenPassword);

            var actual = target.GetEncodedCredentials();

            actual.Should().NotContain(GivenName);
            actual.Should().NotContain(GivenPassword);
        }
    }
}
