using FluentAssertions;

using HM.JiraMetrics.Test.UnitTests;

using NUnit.Framework;

namespace HM.JiraMetrics.Test.Unit
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
