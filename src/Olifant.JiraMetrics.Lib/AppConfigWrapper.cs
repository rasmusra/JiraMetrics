using System;
using System.Configuration;

namespace Olifant.JiraMetrics.Lib
{
    internal static class AppConfigWrapper
    {

        internal static string JiraCredentials
        {
            get
            {
                return ConfigurationManager.AppSettings["JiraCredentials"];
            }
        }

        internal static string JiraBaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["JiraBaseUrl"];
            }
        }

        internal static int MaxResult
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["MaxResult"]);
            }
        }
    }
}
