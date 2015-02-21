using System;
using System.Text;

namespace HM.JiraMetrics.Test.UnitTests
{
    public class JiraCredentials
    {
        private readonly object username;

        private readonly object password;

        public JiraCredentials(object username, object password)
        {
            this.username = username;
            this.password = password;
        }

        public string GetEncodedCredentials()
        {
            var loginCredentials = string.Format("{0}:{1}", this.username, this.password);
            byte[] byteCredentials = UTF8Encoding.UTF8.GetBytes(loginCredentials);
            return Convert.ToBase64String(byteCredentials);
        }
    }
}